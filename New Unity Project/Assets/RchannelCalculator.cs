using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;
using System.Threading.Tasks;
using Unity.Mathematics;
using System.Threading;


public class Data
{
    public float total = 0;
}

public class RchannelCalculator : MonoBehaviour
{
    public Material material;

    public Texture mainTexture;

    public RchannelCalculatorJob[] jobs = new RchannelCalculatorJob[4];

    public RchannelCalculatorJob2[] jobs_ijob = new RchannelCalculatorJob2[4];

    public static volatile float[] jobs_calulation = new float[4];

    private List<JobHandle> handles = new List<JobHandle>();

    bool started = false;

    float startTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainTexture = material.GetTexture("_MainTex");

        Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
        Graphics.Blit(mainTexture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        Color[] pixels = texture2D.GetPixels();

        RenderTexture.active = currentRT;

        int width_half = mainTexture.width / 2;
        int height_half = mainTexture.height / 2;

        //validate if the width and height are even
        int x_fee = 0;// (width_half % 2);
        int y_fee = 0;// (height_half % 2);


        int xpos = 0;
        int ypos = 0;

        startTime = Time.time;

        jobs_ijob[0] = new RchannelCalculatorJob2()
        {
            colors = new NativeArray<Color>(texture2D.GetPixels(xpos, ypos, width_half, height_half), Allocator.Persistent),
            result = new NativeArray<float>(1, Allocator.TempJob),
            index = 0,
        };



        xpos = width_half;
        ypos = 0;

        jobs_ijob[1] = new RchannelCalculatorJob2()
        {
            colors = new NativeArray<Color>(texture2D.GetPixels(xpos+ x_fee, ypos, width_half, height_half), Allocator.Persistent),
            result = new NativeArray<float>(1, Allocator.TempJob),
            index = 1,
        };

        xpos = 0;
        ypos = height_half;



        jobs_ijob[2] = new RchannelCalculatorJob2()
        {
            colors = new NativeArray<Color>(texture2D.GetPixels(xpos, ypos+y_fee, width_half, height_half), Allocator.Persistent),
            result = new NativeArray<float>(1, Allocator.TempJob),
            index = 2,

        };
        xpos = width_half;
        ypos = height_half;

        jobs_ijob[3] = new RchannelCalculatorJob2()
        {
            colors = new NativeArray<Color>(texture2D.GetPixels(xpos + x_fee, ypos + y_fee, width_half, height_half), Allocator.Persistent),
            result = new NativeArray<float>(1, Allocator.TempJob),
            index = 3,

        };



        foreach (var job in jobs_ijob)
        {

            handles.Add(job.Schedule());

        }


        foreach (var job in this.handles)
        {
            job.Complete();
        }


        float totalt = 0;

        foreach (var job in this.jobs_ijob)
        {
            totalt += job.result[0];

        }

        Debug.Log("Total Rchannel " + +totalt + " " + "; tiempo " + (Time.time - startTime) + " s");
       
        float totalt2 = 0;
        foreach (var job in RchannelCalculator.jobs_calulation)
        {
            totalt2 += job;
        }
        Debug.Log("Total Rchannel2 " + +totalt2 );


        foreach (var job in jobs_ijob)
        {
            if (job.colors != null)
                job.colors.Dispose();


            if (job.result != null)
                job.result.Dispose();



        }
        this.enabled = false;


    }


    void Update()
    {

        /*
        if (!started)
        {
            startTime = Time.time;

            foreach (var job in jobs2)
            {

                handles.Add(job.Schedule());

            }
            started = true;

            return;

        }


        bool allhavefinised = true;

        for (int i = 0; i < handles.Count && allhavefinised; i++)
        {
            var handle = handles[i];
            allhavefinised = allhavefinised && handle.IsCompleted;
        }

        if (allhavefinised)
        {
            //COMPLETE ES UN METODO BLOQUEANTE , NO ESTOY SEGURO SI ES MANDATORIO LLAMARLO ENTONCES MEJOR LO LLAMO
            foreach (var job in this.handles)
            {
                job.Complete();
            }




            float totalt = 0;

            foreach (var job in this.jobs2)
            {
                totalt += job.result[0];

            }



            Debug.Log("Total Rchannel " + +totalt + " " + "; tiempo " + (Time.time - startTime) + " s");
            this.enabled = false;
        }

        */
    }
    /*
    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            startTime = Time.time;

            foreach (var job in jobs)
            {

                handles.Add(job.Schedule(job.colors.Length, 10));

            }
            started = true;

            return;

        }


        bool allhavefinised = true;

        for (int i = 0; i < handles.Count && allhavefinised; i++)
        {
            var handle = handles[i];
            allhavefinised = allhavefinised && handle.IsCompleted;
        }

        if (allhavefinised)
        {

            foreach (var job in this.handles)
            {
                job.Complete();


            }



            float totalt2 = 0;


            foreach (var job in RchannelCalculator.jobs_calulation)
            {

                totalt2 += job;
            }


            float totalt = 0;

            foreach (var job in this.jobs)
            {
                totalt += job.totalf;

            }



            Debug.Log("Total Rchannel "+ totalt2 +" "+ +totalt+" " + "; tiempo " + (Time.time - startTime) + " s");
            this.enabled = false;
        }
    }
    */

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
       /*
            foreach (var job in jobs2)
            {
                if (job.colors != null)
                    job.colors.Dispose();


                if (job.result != null)
                    job.result.Dispose();

            }
            */
            Debug.Log("job.colors.Disposed");
     
    }

    public struct RchannelCalculatorJob2 : IJob
    {
        public NativeArray<Color> colors;
        public NativeArray<float> result;
        public int index;
        public void Execute()
        {
            float total = 0;
            for (int i = 0; i < colors.Length; i++)
            {


                total += colors[i].r;
                RchannelCalculator.jobs_calulation[index] += colors[i].r;



            }
            result[0] = total;

            
        }
    }


    // [BurstCompile]
    public struct RchannelCalculatorJob : IJobParallelFor
    {


        public NativeArray<Color> colors;
        //public NativeArray<float> val;



        public int index;

        public float totalf;



        public int counter;



        public void Execute(int i)
        {
            var color = colors[i];

            counter++;

            totalf = totalf + color.r;


            if (color.r > 0)
            {
                var i2 = 0;
            }


            RchannelCalculator.jobs_calulation[index] = RchannelCalculator.jobs_calulation[index] + color.r;


            //data.total +=color.r;
        }

    }


}

