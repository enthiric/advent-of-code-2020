using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class NavigationalComputer
    {
        public List<Instruction> Instructions;

        public (Coordinate coordinate, Direction shipDirection) FollowOriginal(Coordinate current,
            Direction shipDirection,
            Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.N:
                    current.Y += instruction.Value;
                    break;
                case Direction.S:
                    current.Y -= instruction.Value;
                    break;
                case Direction.E:
                    current.X += instruction.Value;
                    break;
                case Direction.W:
                    current.X -= instruction.Value;
                    break;
                case Direction.F:
                    return FollowOriginal(
                        current,
                        shipDirection, new Instruction
                        {
                            Direction = shipDirection,
                            Value = instruction.Value
                        }
                    );
                case Direction.L:
                    shipDirection = FollowLeft(shipDirection, instruction);
                    break;
                case Direction.R:
                    shipDirection = FollowRight(shipDirection, instruction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (current, shipDirection);
        }

        public (Coordinate coordinate, Coordinate waypoint, Direction shipDirection) FollowWaypoint(Coordinate current,
            Coordinate waypoint, Direction shipDirection,
            Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.N:
                    waypoint.Y += instruction.Value;
                    break;
                case Direction.S:
                    waypoint.Y -= instruction.Value;
                    break;
                case Direction.E:
                    waypoint.X += instruction.Value;
                    break;
                case Direction.W:
                    waypoint.X -= instruction.Value;
                    break;
                case Direction.F:
                    current.X += waypoint.X * instruction.Value;
                    current.Y += waypoint.Y * instruction.Value;
                    break;
                case Direction.L:
                    for (var i = 0; i < instruction.Value / 90; i++)
                    {
                        waypoint.X = waypoint.Y;
                        waypoint.Y = waypoint.X * -1;
                    }

                    shipDirection = FollowLeft(shipDirection, instruction);
                    break;
                case Direction.R:
                    for (var i = 0; i < instruction.Value / 90; i++)
                    {
                        waypoint.X = waypoint.Y * -1;
                        waypoint.Y = waypoint.X;
                    }

                    shipDirection = FollowRight(shipDirection, instruction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (current, waypoint, shipDirection);
        }

        private Direction FollowRight(Direction shipDirection, Instruction instruction)
        {
            for (var i = 0; i < instruction.Value / 90; i++)
            {
                shipDirection = shipDirection switch
                {
                    Direction.N => Direction.E,
                    Direction.S => Direction.W,
                    Direction.E => Direction.S,
                    Direction.W => Direction.N,
                    _ => throw new ArgumentOutOfRangeException(nameof(shipDirection), shipDirection, null)
                };
            }

            return shipDirection;
        }

        private Direction FollowLeft(Direction shipDirection, Instruction instruction)
        {
            for (var i = 0; i < instruction.Value / 90; i++)
            {
                shipDirection = shipDirection switch
                {
                    Direction.N => Direction.W,
                    Direction.S => Direction.E,
                    Direction.E => Direction.N,
                    Direction.W => Direction.S,
                    _ => throw new ArgumentOutOfRangeException(nameof(shipDirection), shipDirection, null)
                };
            }

            return shipDirection;
        }

        public static NavigationalComputer Parse(string[] input)
        {
            return new NavigationalComputer
            {
                Instructions = input.Select(Instruction.Parse).ToList()
            };
        }
    }
}