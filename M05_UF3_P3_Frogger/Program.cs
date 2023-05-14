using System;
using System.Collections.Generic;
using System.Linq;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<Lane> ListaTroncos = new List<Lane>();
            List<Lane> ListaCoches = new List<Lane>();

            GenElementos(ListaTroncos, ListaCoches);
        }

        static void DrawCesped(int Inicio, int Filas = 2, int Frames = 1)
        {
            List<List<Lane>> ListaCesped = new List<List<Lane>>();

            for (int i = 0; i < Filas; i++)
            {
                List<Lane> Cesped = new List<Lane>();

                for (int x = 0; x < Frames; x++)
                {
                    Cesped.Add(new Lane(Inicio + i, true, ConsoleColor.Green, false, true, 1f, Utils.charGrass, Utils.colorsGrass.ToList()));
                };

                ListaCesped.Add(Cesped);
            };

            for (int y = 0; y < Frames; y++)
            {
                for (int v = 0; v < Filas; v++)
                {
                    ListaCesped[v][y].Update();
                    ListaCesped[v][y].Draw();
                }
                // Console.Write("\nFrame: " + y);
            }
        }

        static void GenElementos(List<Lane> ListaTroncos, List<Lane> ListaCoches, int FilasTroncos = 5, int FilasCoches = 5, int InicioTroncos = 2, int InicioCoches = 7)
        {
            for (int i = 0; i < FilasTroncos; i++)
            {
                ListaTroncos.Add(new Lane(InicioTroncos + i, true, ConsoleColor.DarkBlue, false, true, .3f, Utils.charLogs, Utils.colorsLogs.ToList()));
            };

            for (int i = 0; i < FilasCoches; i++)
            {
                ListaCoches.Add(new Lane(InicioCoches + i, true, ConsoleColor.Black, true, false, .1f, Utils.charCars, Utils.colorsCars.ToList()));
            };
        }

        static void DrawCoches(int Inicio, int Filas = 5, int Frames = 10)
        {
            List<List<Lane>> ListaCoches = new List<List<Lane>>();

            for (int i = 0; i < Filas; i++)
            {
                List<Lane> Coches = new List<Lane>();

                for (int x = 0; x < Frames; x++)
                {
                    Coches.Add(new Lane(Inicio + i, false, ConsoleColor.Black, true, false, .1f, Utils.charCars, Utils.colorsCars.ToList()));
                };

                ListaCoches.Add(Coches);
            };

            for (int y = 0; y < Frames; y++)
            {
                for (int v = 0; v < Filas; v++)
                {
                    ListaCoches[v][y].Draw();
                }
                // Console.Write("\nFrame: " + y);
            }
        }

    }
}
