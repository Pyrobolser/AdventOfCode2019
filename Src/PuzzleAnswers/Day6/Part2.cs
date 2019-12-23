using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.PuzzleAnswers.Day6
{
    public static class Part2
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

            public static int TransfersBetween(SpaceObject start, SpaceObject end)
            {
                int leftSteps = -1, rightSteps = -1;
                HashSet<SpaceObject> chartedSpace = new HashSet<SpaceObject>();
                HashSet<SpaceObject> lefts = new HashSet<SpaceObject> { start };
                HashSet<SpaceObject> rights = new HashSet<SpaceObject> { end };

                while (rights.Intersect(lefts).Count() == 0)
                {
                    var tempLefts = new HashSet<SpaceObject>();
                    var tempRights = new HashSet<SpaceObject>();
                    chartedSpace.UnionWith(lefts);
                    chartedSpace.UnionWith(rights);

                    foreach (var left in lefts)
                    {
                        tempLefts.UnionWith(left.OrbitedBy);
                        tempLefts.Add(left.Orbits);

                        tempLefts.ExceptWith(chartedSpace);
                    }
                    leftSteps++;

                    foreach(var right in rights)
                    {
                        tempRights.UnionWith(right.OrbitedBy);
                        tempRights.Add(right.Orbits);

                        tempRights.ExceptWith(chartedSpace);
                    }
                    rightSteps++;

                    lefts = tempLefts;
                    rights = tempRights;
                }

                return leftSteps + rightSteps;
            }

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

            var me = space.FirstOrDefault(so => so.Name == "YOU");
            var santa = space.FirstOrDefault(so => so.Name == "SAN");

            return SpaceObject.TransfersBetween(me, santa);
        }
    }
}