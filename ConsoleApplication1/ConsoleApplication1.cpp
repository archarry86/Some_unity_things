// ConsoleApplication1.cpp : Este archivo contiene la función "main". La ejecución del programa comienza y termina ahí.
//



// Standard Library imports
#include "iostream"
#include <string>
#include <vector>

using namespace std;

char* reverseConstString(char const* str)
{
    // find length of string 
    int n = strlen(str);

    // create a dynamic pointer char array 


    char* rev = new char[n];

    vector<char> vector;



    for (int j = n - 1, i = 0; j >= 0; j--, i++) {
        int intvalue = (int)str[i];
        if (intvalue >= 0) {
            rev[j] = str[i];
            vector.push_back(str[i]);
        }
        else
        {
            char a = str[i];
            i++;
            char b = str[i];
            rev[j] = b;
            vector.push_back(b);
            j--;
            rev[j] = a;
            vector.push_back(a);
            //j--; i++;
            //rev[j] = str[i];
        }
    }

  //  return reinterpret_cast<char*> (&vector);

    // return pointer of the reversed string 
   return rev;
}

char* reverseConstString2(int * str, int n)
{
    // find length of string 
   

    // create a dynamic pointer char array 
    char* rev = new char[n];

    vector<char> vector;



    for (int j = n - 1, i = 0; j >= 0; j--, i++) {
        int intvalue = (int)str[i];
        if (intvalue >= 0) {
            rev[j] = str[i];
            vector.push_back(str[i]);
        }
        else
        {
            char a = str[i];
            i++;
            char b = str[i];
            rev[j] = b;
            vector.push_back(b);
            j--;
            rev[j] = a;
            vector.push_back(a);
            //j--; i++;
            //rev[j] = str[i];
        }
    }

   // return reinterpret_cast<char*> (&vector);

    // return pointer of the reversed string 
    return rev;
}

int main()
{

    
    const char* s = "Añ";

  

    printf("%s", reverseConstString(s));

  
    return (0);
   
}




// Ejecutar programa: Ctrl + F5 o menú Depurar > Iniciar sin depurar
// Depurar programa: F5 o menú Depurar > Iniciar depuración

// Sugerencias para primeros pasos: 1. Use la ventana del Explorador de soluciones para agregar y administrar archivos
//   2. Use la ventana de Team Explorer para conectar con el control de código fuente
//   3. Use la ventana de salida para ver la salida de compilación y otros mensajes
//   4. Use la ventana Lista de errores para ver los errores
//   5. Vaya a Proyecto > Agregar nuevo elemento para crear nuevos archivos de código, o a Proyecto > Agregar elemento existente para agregar archivos de código existentes al proyecto
//   6. En el futuro, para volver a abrir este proyecto, vaya a Archivo > Abrir > Proyecto y seleccione el archivo .sln
