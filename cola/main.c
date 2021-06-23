#include <stdlib.h>
#include "cola.h"
#include <stdio.h>

int main(){
    COLA *c = crear_cola();
    enqueue(c,1);
    enqueue(c,2);
    enqueue(c,3);
    enqueue(c,4);
    imprimir_cola(c);
    enqueue(c,5);
    imprimir_cola(c);
    vaciar(c);
    imprimir_cola(c);
}