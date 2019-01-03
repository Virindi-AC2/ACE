using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using ACE.Database.Models.World;

namespace ACE.Adapter.Lifestoned
{
    public static class LifestonedLoader
    {
        public static bool TryLoadWeenie(string file, out global::Lifestoned.DataModel.Gdle.Weenie result)
        {
            try
            {
                var fileText = File.ReadAllText(file);

                result = JsonConvert.DeserializeObject<global::Lifestoned.DataModel.Gdle.Weenie>(fileText);

                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static bool TryLoadWeenies(string folder, out List<global::Lifestoned.DataModel.Gdle.Weenie> results)
        {
            results = new List<global::Lifestoned.DataModel.Gdle.Weenie>();

            try
            {
                var files = Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories).OrderByDescending(f => new FileInfo(f).CreationTime).ToList();

                foreach (var file in files)
                {
                    if (TryLoadWeenie(file, out var result))
                        results.Add(result);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryLoadWeeniesInParallel(string folder, out List<global::Lifestoned.DataModel.Gdle.Weenie> results)
        {
            try
            {
                var files = Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories);

                var weenies = new ConcurrentBag<global::Lifestoned.DataModel.Gdle.Weenie>();

                Parallel.ForEach(files, file =>
                {
                    if (TryLoadWeenie(file, out var result))
                        weenies.Add(result);
                });

                results = new List<global::Lifestoned.DataModel.Gdle.Weenie>(weenies);

                return true;
            }
            catch
            {
                results = null;
                return false;
            }
        }


        public static bool TryLoadWeenieConverted(string file, out Weenie result)
        {
            try
            {
                var fileText = File.ReadAllText(file);

                var lifestonedModel = JsonConvert.DeserializeObject<global::Lifestoned.DataModel.Gdle.Weenie>(fileText);

                return LifestonedConverter.TryConvert(lifestonedModel, out result);
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static bool TryLoadWeeniesConverted(string folder, out List<Weenie> results)
        {
            results = new List<Weenie>();

            try
            {
                var files = Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories).OrderByDescending(f => new FileInfo(f).CreationTime).ToList();

                foreach (var file in files)
                {
                    if (TryLoadWeenieConverted(file, out var result))
                        results.Add(result);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryLoadWeeniesConvertedInParallel(string folder, out List<Weenie> results)
        {
            try
            {
                var files = Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories);

                var weenies = new ConcurrentBag<Weenie>();

                Parallel.ForEach(files, file =>
                {
                    if (TryLoadWeenieConverted(file, out var result))
                        weenies.Add(result);
                });

                results = new List<Weenie>(weenies);

                return true;
            }
            catch
            {
                results = null;
                return false;
            }
        }
    }
}