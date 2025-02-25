﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

           string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                logger.LogError("File has no input");
            }

            if (lines.Length == 1)
            {
                logger.LogWarning("File only has one line of input");
            }

            logger.LogInfo($"Lines: {lines[0]}");


            var parser = new TacoParser();

  
            var locations = lines.Select(parser.Parse).ToArray();


            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double distance = 0;



            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate();
                    corA.Latitude = locB.Location.Latitude;
                    corA.Longitude = locB.Location.Longitude;
                
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }

                //logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart");
            }
            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart");

        }
    }
}
