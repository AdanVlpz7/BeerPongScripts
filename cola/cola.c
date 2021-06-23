#include "cola.h"
#include <stdio.h>
#include <stdlib.h>

// crear nodo
NODE* new_node(DATA info){
   NODE* t = (NODE*)malloc(sizeof(NODE));
    t->siguiente = NULL;
    t->info = info;
    return t;
} 

//eliminar nodo
void free_node(NODE *n){
    if(n->siguiente == NULL){
        free(n);
        n = NULL;
    }
} 
//crear lista
COLA* crear_cola(){
    COLA* l = (COLA*)malloc(sizeof(COLA));
    l->head = l->tail = NULL;
    l->num = 0;
    return l;
} 

//para verificar si la lista esta vacia.
bool is_empty(COLA*l){
    if(l->head == l->tail && l->tail == NULL)
        return true;
    else
        return false;
}



bool enqueue(COLA *l, DATA info){
    NODE* nuevo = new_node(info);
    if(l->head == NULL && l->tail==NULL){
        l->head = l->tail = nuevo; // l->head = nuevo; l->tail = nuevo;
        l->num++;
        return true;
    }else{
        l->tail->siguiente = nuevo;
        l->tail = nuevo;// l->tail = l->tail->sig;
        l->num++;
        return true;
    }
    return false;
}
bool vaciar(COLA *l){
    if(is_empty(l)) return false;
    NODE* temporal = l->head;
    while(temporal !=NULL){
        l->head = temporal->siguiente;
        free(temporal);
        temporal = l->head;
    }
    l->head = l->tail = NULL;
    l->num = 0;
    return true;
}

void eliminarInicio(COLA *l){
    //si la lista ya esta vacia no podemos cambiar nada
    if(is_empty(l)) return;
    NODE *temporal = l->head;
    l->head = l->head->siguiente;
    temporal->siguiente = NULL;
    free_node(temporal);
    l->num--;
}


DATA* first(COLA* l, int pos){
    if(is_empty(l)) return NULL;
    if(pos == 0) return &l->head->info;
    if(pos == l->num-1) return &l->tail->info;
    if(pos > 0 && pos < (l->num-1)){
        NODE *temporal = l->head;
        for(int i = 0; i < pos; i++){
            temporal = temporal->siguiente;
        } 
        return &temporal->info;
    }
}

int localizar(COLA *l, DATA info){
    if(is_empty(l)) return -1;
    NODE *temporal = l->head;
    int pos = 0;
    while(temporal != NULL){
        if(comparar(temporal->info, info)) return pos;
        pos++;
        temporal = temporal->siguiente;
    }
    return -1;
}
bool comparar(DATA info1, DATA info2){
    if(info1 == info2) return true;
    else return false;
}
void imprimir_cola(COLA *l){
    for(NODE* t = l->head;t != NULL; t = t->siguiente){
        printf("%d ->", t->info);
    }
    printf("\n");
}