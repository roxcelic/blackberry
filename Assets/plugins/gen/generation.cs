using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace generation {
    public class var {
        public static Dictionary<string, string> conversion = new Dictionary<string, string> {
            {"0", "90"},
            {"1", "11"},
            {"2", "22"},
            {"3", "33"},
            {"4", "44"},
            {"5", "55"},
            {"6", "66"},
            {"7", "77"},
            {"8", "88"},
            {"9", "99"},
            {"A", "91"},
            {"B", "92"},
            {"C", "93"},
            {"D", "94"},
            {"E", "95"},
            {"F", "96"},
            {"G", "97"},
            {"H", "98"},
            {"I", "99"},
            {"J", "10"},
            {"K", "11"},
            {"L", "12"},
            {"M", "13"},
            {"N", "14"},
            {"O", "15"},
            {"P", "16"},
            {"Q", "17"},
            {"R", "18"},
            {"S", "19"},
            {"T", "20"},
            {"U", "21"},
            {"V", "22"},
            {"W", "23"},
            {"X", "24"},
            {"Y", "25"},
            {"Z", "26"}
        };

    }

    public class utils {
        public static float charToFloat(char character){
            return float.Parse(character.ToString());
        }

        public static int opositeIndex(int index, long seed){
            int seedLength = (seed.ToString()).Length - 1;
            int halfSeed = seedLength / 2;

            return index > halfSeed ? seedLength - index : seedLength - index;
        }
        
        public static int randomIndex(int length, int choice, long seed) {
            string stringseed = seed.ToString();
            
            // generate a number between 0 and 17 based on the seed
            int index = (int)utils.charToFloat(stringseed[choice]);
            float val = utils.charToFloat(stringseed[index]) + utils.charToFloat(stringseed[opositeIndex(index, seed)]);

            float percentagemultiplier = length / 18f;

            int returnedindex = (int)Mathf.Clamp(val * percentagemultiplier, 1, length);

            // returns the index
            return returnedindex;
        }
    }

    public class seed {
        public static long Convert(string seed) {
            // converts the seed to uppercase
            seed = seed.ToUpper();

            string expandedSeed = "";

            foreach (char c in seed){expandedSeed += var.conversion[c.ToString()];}

            return long.Parse(expandedSeed);
        }

        public static long incrementRoom(long seed, int roomNum) {
            long HalfSeed = seed / 2;
            int start = int.Parse(((seed.ToString())[0]).ToString());
            double multiplier = double.Parse($"1.0{roomNum.ToString()}");
            double expand = HalfSeed * multiplier;
            double trueSeed = 0.0;

            trueSeed = (start > 5 ? (seed - expand) : (seed + expand));

            return (long)trueSeed;
        }

        public static long incrementFloor(long seed, int roomNum) {
            long HalfSeed = seed / 2;
            int start = int.Parse(((seed.ToString())[0]).ToString());
            double multiplier = double.Parse($"1.2{roomNum.ToString()}");
            double expand = HalfSeed * multiplier;
            double trueSeed = 0.0;

            trueSeed = (start > 5 ? (seed - expand) : (seed + expand));

            return (long)trueSeed;
        }

        public static long incrementItem(long seed) {
            long HalfSeed = seed / 2;
            int start = int.Parse(((seed.ToString())[0]).ToString());
            double multiplier = double.Parse($"1.05");
            double expand = HalfSeed * multiplier;
            double trueSeed = 0.0;

            trueSeed = (start > 5 ? (seed - expand) : (seed + expand));

            return (long)trueSeed;
        }
        
        public static string Check(string seed) {
            if (seed.Length == 6) {
                return seed;
            } else {
                return "ffffff";
            }
        }
    }

    public class generation {

        // test
        public static void test(string test) {Debug.Log(test);}
    }

}