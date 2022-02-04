﻿/*
 * Author: Sephora Bateman 
 * Class: CS 4150
 * Problem Set #3
 */

/*
 * WHAT TO WORK ON FOR CODE:
 *  - fix islandsANdFerryData method so it is the correct amount in the dictionary.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_3
{
    public class Ps3submission
    {
        public static long nofDestinations;
        public static long passedDes = 0;
        public static int minislands;
        public static Dictionary<int,int> islandsAndFerries = new Dictionary<int, int>();
        public static HashSet<int> islands = new HashSet<int>();
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string line;
            int index = 1;

            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);

            for (int i = 1; i < numOfFerryRoutes; i++)
                inputIslandAndFerryData(i); // store the islands and their data into some data sturctures. 
            

            findMinNumOfShops(islands,passedDes,index);

            // Print the min number of islands and the islands it takes. 
            Console.WriteLine(minislands);
            for (int i = 1; i <= numOfDestinations; i++)
                if ((passedDes & (1 << i)) != 0)
                    Console.WriteLine(i + ' ');
        }
        /// <summary>
        ///  Stores the islands and the ferry routes in a dictionary also figure out the destination. 
        /// </summary>
        /// <param name="i"></param>
        private static void inputIslandAndFerryData(int i)
        {
            string line = Console.ReadLine();
            string[] spilt = line.Split(' ');
            int island = Int32.Parse(spilt[0]);
            int ferry = Int32.Parse(spilt[1]);
            if (i <= numOfDestinations)
                nofDestinations = nofDestinations | (1 << i);

            // inculde orphan islands. 
            if (!islandsAndFerries.ContainsKey(island))
            {
                islandsAndFerries.Add(island, ferry);
                islandsAndFerries.Add(ferry, island);
            }
            else
            {
                islandsAndFerries.TryGetValue(island, out int value);
                islandsAndFerries.Remove(island);
                int input = (value | (1 << ferry)) + (value | (1 << island));
                islandsAndFerries.Add(island, input);
                islandsAndFerries.Add(ferry, (1 << island));
            }
        }

        /// <summary>
        /// Finds the smallest number of shops that need to be built. 
        /// </summary>
        /// <param name="islands"> the list of islands.</param>
        /// <param name="passedDes">The islands we have passed</param>
        /// <param name="index">the island we are checking.</param>
        private static void findMinNumOfShops(HashSet<int> islands, long passedDes, int index)
        {
            if (index > numOfDestinations) //
               return;

            if (passedDes == nofDestinations) // base case
                return;

            islandsAndFerries.TryGetValue(index, out int value);
            for (int i = 1; i <= numOfDestinations; i++)
                if (((value & (1 << i)) != 0) && (passedDes & (1 << i)) !=1)
                    passedDes = passedDes | (1 << i);

            // also inculde the orphan islands. 

            findMinNumOfShops(islands,0, index+1);
            islands.Add(index);
            findMinNumOfShops(islands, passedDes, index+1);
        }
    }
}
