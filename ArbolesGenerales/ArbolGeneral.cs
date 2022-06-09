﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesGenerales
{
    public class ArbolGeneral
    {
        private readonly Nodo raiz;

        //public Nodo Raiz => raiz; (hace lo mismo que el de abajo(expresiones landa))
        public Nodo Raiz { get { return raiz; } }
        public ArbolGeneral(string dato)
        {
            raiz = new Nodo (dato);
        }

        public Nodo InsertarHijo(Nodo padre, string dato)
        {
            if (string.IsNullOrWhiteSpace(dato))
            {
                throw new Exception("El dato esta vacio");
            }

            if(padre is null)
            {
                throw new Exception("El padre no puede ser nulo");   
            }
            
            if(padre.Hijo is null)
            {
                padre.Hijo = new Nodo (dato);
                return padre.Hijo;
            }
            else
            {
                Nodo hijoActual = padre.Hijo;
                while (hijoActual.Hermano is not null)
                {
                    hijoActual = hijoActual.Hermano;
                }
                hijoActual.Hermano = new Nodo (dato);
                return hijoActual.Hermano;
            }
        }

        private void Recorrer(Nodo nodo, ref int posicion, ref string datos)
        {
           if(nodo != null)
            {
                string dato = nodo.Dato;
                int cantidadGuiones = dato.Length + posicion;
                string datoConGuiones = dato.PadLeft(cantidadGuiones, '-');
                datos += $"{datoConGuiones}\n";
                
                if(nodo.Hijo != null)
                {
                    posicion++;
                    Recorrer(nodo.Hijo, ref posicion, ref datos);
                    posicion--;

                }

                if(nodo.Hermano != null && posicion != 0)
                {
                    Recorrer (nodo.Hermano, ref posicion, ref datos);
                }
            }
        }

        public string ObtenerArbol(Nodo nodo = null)
        {
            if(nodo == null)
            {
                nodo = raiz;
            }

            int posicion = 0;
            string datos = "";
            Recorrer(nodo, ref posicion, ref datos);
            return datos;
        }

        public Nodo Buscar(string dato, Nodo nodoBusqueda = null)
        {
            if(nodoBusqueda == null)
            {
                nodoBusqueda = raiz;
            }

            if(nodoBusqueda.Dato.ToUpper() == dato.ToUpper())
            {
                return nodoBusqueda;
            }

            if(nodoBusqueda.Hijo != null)
            {
                Nodo nodoEncontrado = Buscar (dato, nodoBusqueda.Hijo);
                
                if(nodoEncontrado != null)
                {
                    return nodoEncontrado;
                }
            }

            if(nodoBusqueda.Hermano != null)
            {
                Nodo nodoEncontrado = Buscar(dato, nodoBusqueda.Hermano);
                
                if (nodoEncontrado != null)
                {
                    return nodoEncontrado;
                }
            }
            return null;
        }
    }
}
