using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string WinString = 
            @"00000000000000000000
            00001010111010100000
            00001010101010100000
            00000100101010100000
            00000100101010100000
            00000100111011100000
            00000000000000000000
            00010001011101001000
            00010001001001101000
            00010101001001011000
            00010101001001001000
            00001010011101001000
            00000000000000000000";

            string LooseString = 
            @"00000000000000000000
            01110111010001011100
            01000101011011010000
            01010111010101011000
            01010101010001010000
            01110101010001011100
            00000000000000000000
            00111010101110111000
            00101010101000101000
            00101010101100110000
            00101010101000101000
            00111001001110101000
            00000000000000000000";

            string response = "no";

            do
            {
                Console.Clear();
                
                List<Lane> Map = new List<Lane>();
                Player jugador = new Player();

                Utils.GAME_STATE Estado = new Utils.GAME_STATE();
                Estado = Utils.GAME_STATE.WIN;

                // ====== SETUP ====== 

                GenElementos(Map);



                // ====== LOOP ====== 
                do
                {
                    DrawMap(Map);
                    Estado = DrawPlayer(jugador, Map);

                    // DEBUG
                    Console.ResetColor();
                    //Console.SetCursorPosition(0, 15);
                    //Console.WriteLine(Estado);

                    // FRAME MANAGEMENT
                    TimeManager.NextFrame();

                } while (Estado == Utils.GAME_STATE.RUNNING);

                // ====== RESULT ====== 
                if (Estado == Utils.GAME_STATE.WIN)
                {
                    DrawBitMap(WinString, ConsoleColor.Green);
                }
                else if (Estado == Utils.GAME_STATE.LOOSE)
                {
                    DrawBitMap(LooseString, ConsoleColor.Red);
                }

                Console.Write("\nPor Alejandro Vazquez");
                Console.Write("\nQuieres Jugar de nuevo? (si/no): ");
                response = Console.ReadLine();

            } while (response == "si");
            

        }

        // MAP SETUP
        public static void DrawMap(List<Lane> Map)
        {
            foreach (var linea in Map)
            {
                linea.Draw();
                linea.Update();
            }

        }

        public static void GenElementos(List<Lane> Map, int FilasCesped = 1, int FilasTroncos = 5, int FilasCoches = 5)
        {
            if (FilasCesped * 3 + FilasTroncos + FilasCoches > Utils.MAP_HEIGHT)
            {
                Console.Write("ERROR: SE EXCEDE LA ALTURA MAXIMA DE EL MAPA!");
            }
            else
            {
                int InicioCesped = 0;
                for (int i = 0; i < FilasCesped; i++)
                {
                    Map.Add(new Lane(InicioCesped + i, false, ConsoleColor.DarkGreen, false, false, 1f, Utils.charGrass, Utils.colorsGrass.ToList()));
                }

                int InicioTroncos = FilasCesped;
                for (int i = 0; i < FilasTroncos; i++)
                {
                    Map.Add(new Lane(InicioTroncos + i, true, ConsoleColor.DarkBlue, false, true, .7f, Utils.charLogs, Utils.colorsLogs.ToList()));
                };

                InicioCesped = FilasCesped + FilasTroncos;
                for (int i = 0; i < FilasCesped; i++)
                {
                    Map.Add(new Lane(InicioCesped + i, false, ConsoleColor.DarkGreen, false, false, 1f, Utils.charGrass, Utils.colorsGrass.ToList()));
                }

                int InicioCoches = FilasCesped * 2 + FilasTroncos;
                for (int i = 0; i < FilasCoches; i++)
                {
                    Map.Add(new Lane(InicioCoches + i, false, ConsoleColor.Black, true, false, .1f, Utils.charCars, Utils.colorsCars.ToList()));
                };

                InicioCesped = FilasCesped + FilasTroncos + FilasCesped + FilasCoches;
                for (int i = 0; i < FilasCesped; i++)
                {
                    Map.Add(new Lane(InicioCesped + i, false, ConsoleColor.DarkGreen, false, false, 1f, Utils.charGrass, Utils.colorsGrass.ToList()));
                }

            }


        }
    
        // PLAYER SETUP
        public static Utils.GAME_STATE DrawPlayer(Player jugador, List<Lane> Map)
        {
            Utils.GAME_STATE Estado = jugador.Update(Utils.Input(), Map);
            jugador.Draw(Map);
            return Estado;
        }

        // MENSAJES
        public static void DrawBitMap(String BitMap, ConsoleColor TextColor)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = TextColor;

            Console.SetCursorPosition(0, 0);

            int count = 0;

            foreach (char C in BitMap)
            {
                if (C == '1')
                {
                    Console.Write('█');
                    count++;
                }
                else if (C == '0')
                {
                    Console.Write(' ');
                    count++;
                }

                if (count == 20)
                {
                    Console.Write('\n');
                    count = 0;
                }
            }

            Console.ResetColor();
        }
    
    }
}
