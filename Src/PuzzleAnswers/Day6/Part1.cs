using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.PuzzleAnswers.Day6
{
    public static class Part1
    {
        internal class SpaceObject
        {
            public SpaceObject(string name, SpaceObject orbits = null, SpaceObject orbitedBy = null)
            {
                Name = name;
                Orbits = orbits;
                OrbitedBy = new HashSet<SpaceObject>();

                if (orbitedBy != null)
                    OrbitedBy.Add(orbitedBy);
            }

            public string Name { get; }
            public SpaceObject Orbits { get; set; }
            public HashSet<SpaceObject> OrbitedBy { get; }

            public int FindAllOrbits()
            {
                var orbits = -1;
                var current = this;

                while (current != null)
                {
                    current = current.Orbits;
                    orbits++;
                }

                return orbits;
            }

            public override bool Equals(object value)
            {
                return value is SpaceObject spaceObject
                    && string.Equals(spaceObject.Name, Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public static int GetResult()
        {
            var input = File.ReadAllLines("Inputs/Day6.txt").Select(s => s.Split(')'));

            //input = new string[]
            //{
            //    "COM)B",
            //    "B)C",
            //    "C)D",
            //    "D)E",
            //    "E)F",
            //    "B)G",
            //    "G)H",
            //    "D)I",
            //    "E)J",
            //    "J)K",
            //    "K)L"
            //}.Select(s => s.Split(')'));

            var space = new HashSet<SpaceObject>();
            SpaceObject left, right;

            foreach (var orbit in input)
            {
                left = space.FirstOrDefault(so => so.Name == orbit[0]);
                right = space.FirstOrDefault(so => so.Name == orbit[1]);
                
                if (left == null && right == null)
                {
                    left = new SpaceObject(orbit[0], null, null);
                    right = new SpaceObject(orbit[1], left, null);

                    left.OrbitedBy.Add(right);
                    space.Add(left);
                    space.Add(right);
                }
                else if (left == null)
                {
                    left = new SpaceObject(orbit[0], null, right);

                    right.Orbits = left;
                    space.Add(left);
                }
                else if (right == null)
                {
                    right = new SpaceObject(orbit[1], left, null);

                    left.OrbitedBy.Add(right);
                    space.Add(right);
                }
                else
                {
                    left.OrbitedBy.Add(right);
                    right.Orbits = left;
                }
            }

            return space.Sum(so => so.FindAllOrbits());
        }
    }
}
