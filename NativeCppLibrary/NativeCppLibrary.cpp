/* NativeCppLibrary.cpp : Defines the exported functions for the DLL.
*/
#include "pch.h"
#include "framework.h"
#include "NativeCppLibrary.h"

// Standard Library imports
#include "iostream"
#include <string>
#include <vector>
using namespace std;




int displayNumber() {
	return 1;
}
int getRandom() {
	return rand();
}
int displaySum() {
	int first_number = 7;
	int second_number = 7;
	int total = first_number + second_number;
	return total;
}

char* reverseConstString(char const* str)
{
    // find length of string 
    int n = strlen(str);

    // create a dynamic pointer char array 
    char* rev = new char[n];

   

    for (int j = n - 1, i = 0; j >= 0; j--, i++) {
        int intvalue = (int)str[i];
        if (intvalue >= 0) {
            rev[j] = str[i];
          
        }
        else
        {
            char a = str[i];
            i++;
            char b = str[i];
            rev[j] = b;
        
            j--;
            rev[j] = a;
            
        }
    }

 
    // return pointer of the reversed string 
    return rev;
}



