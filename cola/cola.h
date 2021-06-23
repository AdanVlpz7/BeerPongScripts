/*
    * author: Adán Vargas
    * date : 10/03/21
*/
#ifndef COLA_H
#define COLA_H
#include <stdbool.h>

typedef struct _node NODE;
typedef int DATA;

struct _node{
    DATA info; // la información del nodo
    NODE *siguiente; //el apuntador al elemento sig. de la lista
};

typedef struct _cola COLA;

struct _cola{ 
    NODE *head;
    NODE *tail;
    int num;
};

NODE *crear_nodo(DATA info); // crear nodo
void eliminar_nodo(NODE *n); //eliminar nodo

COLA *crear_cola(); //crear lista
void eliminar_cola(COLA *l);
bool enqueue(COLA *l, DATA info);
bool is_empty(COLA *l);
bool vaciar(COLA *l);

void imprimir_cola(COLA *l);
void dequeue(COLA *l);
DATA* first(COLA* l, int pos);
bool comparar(DATA info1, DATA info2);
#endif