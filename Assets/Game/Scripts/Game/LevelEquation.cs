namespace ARMathGame
{
    public static class LevelEquation
    {
        private static bool hundreds = false;

        public static void GetEquation(Grade grade, Levels level, out string equation, out int answer)
        {
            equation = "";
            answer = 0;

            switch (grade)
            {
                case Grade.First:
                    switch (level)
                    {
                        case Levels.Level_1:
                            AddFromAToB2(0, 10, out equation, out answer);
                            break;

                        case Levels.Level_2:
                            AddFromAToB2(0, 18, out equation, out answer);
                            break;

                        case Levels.Level_3:
                            AddFromAToB3(0, 10, out equation, out answer);
                            break;

                        case Levels.Level_4:
                            AddFromAToB2(0, 20, out equation, out answer);
                            break;

                        case Levels.Level_5:
                            AddDigitsFromAToB2(0, 9, 10, out equation, out answer);
                            break;

                        case Levels.Level_6:
                            SubFromAToB2(0, 10, out equation, out answer);
                            break;

                        case Levels.Level_7:
                            SubFromAToB2(0, 18, out equation, out answer);
                            break;

                        case Levels.Level_8:
                            SubDigitsFromAToB2(0, 9, 10, out equation, out answer);
                            break;

                        case Levels.Level_9:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                AddFromAToB2(0, 18, out equation, out answer);
                            }
                            else
                            {
                                SubFromAToB2(0, 18, out equation, out answer);
                            }

                            break;

                        case Levels.Level_10:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                AddDigitsFromAToB2(0, 9, 10, out equation, out answer);
                            }
                            else
                            {
                                SubDigitsFromAToB2(0, 9, 10, out equation, out answer);
                            }

                            break;
                    }

                    break;

                case Grade.Second:
                    switch (level)
                    {
                        case Levels.Level_1:
                            SubFromAToB2(0, 9, out equation, out answer);
                            break;

                        case Levels.Level_2:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                AddFromAToB3(10, 99, out equation, out answer);
                            }
                            else
                            {
                                AddFromAToB2(10, 99, out equation, out answer);
                            }

                            break;

                        case Levels.Level_3:
                            SubFromAToB2(10, 99, out equation, out answer);
                            break;

                        case Levels.Level_4:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                hundreds = true;
                                AddDigitsFromAToB2(1, 9, 100, out equation, out answer);
                            }
                            else
                            {
                            	hundreds = false;
                                Add2(100, 900, 0, 99, out equation, out answer);
                            }

                            break;

                        case Levels.Level_5:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                hundreds = true;
                                SubDigitsFromAToB2(1, 9, 100, out equation, out answer);
                            }
                            else
                            {
                                hundreds = false;
                                Sub2(100, 999, 0, 99, out equation, out answer);
                            }

                            break;

                        case Levels.Level_6:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                AddFromAToB2(0, 20, out equation, out answer);
                            }
                            else
                            {
                                SubFromAToB2(0, 20, out equation, out answer);
                            }

                            break;

                        case Levels.Level_7:
                            if (UnityEngine.Random.Range(0, 2) == 0)
                            {
                                AddFromAToB2(0, 100, out equation, out answer);
                            }
                            else
                            {
                                SubFromAToB2(0, 100, out equation, out answer);
                            }

                            break;

                        case Levels.Level_8:
                            Mult2(0, 10, 0, 10, out equation, out answer);
                            break;

                        case Levels.Level_9:
                            Div2(1, 5, out equation, out answer);
                            break;

                        case Levels.Level_10:
                            Div2(1, 10, out equation, out answer);
                            break;
                    }

                    break;
            }
        }

        private static void AddFromAToB2(int from, int to, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from, to + 1);
            int B = UnityEngine.Random.Range(0, to + 1 - A);

            answer = A + B;
            equation = $"{A} + {B}";
        }

        private static void AddFromAToB3(int from, int to, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from, to + 1);
            int B = UnityEngine.Random.Range(0, to + 1 - A);
            int C = UnityEngine.Random.Range(0, to + 1 - A - B);

            answer = A + B + C;
            equation = $"{A} + {B} + {C}";
        }

        private static void AddDigitsFromAToB2(int from, int to, int digit, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from, to + 1);
            int B = UnityEngine.Random.Range(0, to + 1 - A) * digit;
            A = A * digit;

            answer = A + B;
            equation = $"{A} + {B}";
        }

        private static void SubFromAToB2(int from, int to, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from, to + 1);
            int B = UnityEngine.Random.Range(0, A + 1);

            answer = A - B;
            equation = $"{A} - {B}";
        }

        private static void SubDigitsFromAToB2(int from, int to, int digit, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from, to + 1);
            int B = UnityEngine.Random.Range(0, A + 1) * digit;
            A = A * digit;

            answer = A - B;
            equation = $"{A} - {B}";
        }

        private static void Add2(int from1, int to1, int from2, int to2, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from1, to1 + 1);
            int B = UnityEngine.Random.Range(from2, to2 + 1);

            answer = A + B;
            equation = $"{A} + {B}";
        }

        private static void Sub2(int from1, int to1, int from2, int to2, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from1, to1 + 1);
            int B = UnityEngine.Random.Range(from2, to2 + 1);

            answer = A - B;
            equation = $"{A} - {B}";
        }

        private static void Mult2(int from1, int to1, int from2, int to2, out string equation, out int answer)
        {
            int A = UnityEngine.Random.Range(from1, to1 + 1);
            int B = UnityEngine.Random.Range(from2, to2 + 1);

            answer = A * B;
            equation = $"{A} * {B}";
        }

        private static void Div2(int from, int to, out string equation, out int answer)
        {
            int B = UnityEngine.Random.Range(from, to + 1);
            answer = UnityEngine.Random.Range(0, (100 / to) +1);

            int A = answer * B;

            equation = $"{A} ÷ {B}";
        }

        public static int GetAnswerVariant(Grade grade, Levels level)
        {
            switch (grade)
            {
                case Grade.First:
                    switch (level)
                    {
                        case Levels.Level_1:
                            return UnityEngine.Random.Range(0, 10 + 1);

                        case Levels.Level_2:
                            return UnityEngine.Random.Range(0, 18 + 1);

                        case Levels.Level_3:
                            return UnityEngine.Random.Range(0, 10 + 1);

                        case Levels.Level_4:
                            return UnityEngine.Random.Range(0, 20 + 1);

                        case Levels.Level_5:
                            return UnityEngine.Random.Range(0, 9 + 1) * 10;

                        case Levels.Level_6:
                            return UnityEngine.Random.Range(0, 10 + 1);

                        case Levels.Level_7:
                            return UnityEngine.Random.Range(0, 18 + 1);

                        case Levels.Level_8:
                            return UnityEngine.Random.Range(0, 9 + 1) * 10;

                        case Levels.Level_9:
                            return UnityEngine.Random.Range(0, 18 + 1);

                        case Levels.Level_10:
                            return UnityEngine.Random.Range(0, 9 + 1) * 10;
                    }

                    break;

                case Grade.Second:
                    switch (level)
                    {
                        case Levels.Level_1:
                            return UnityEngine.Random.Range(0, 9 + 1);

                        case Levels.Level_2:
                            return UnityEngine.Random.Range(10, 99 + 1);

                        case Levels.Level_3:
                            return UnityEngine.Random.Range(0, 99 + 1);

                        case Levels.Level_4:
                            if (hundreds)
                            {
                                return UnityEngine.Random.Range(1, 9 + 1) * 100;
                            }
                            else
                            {
                                return UnityEngine.Random.Range(100, 999 + 1);
                            }

                        case Levels.Level_5:
                            if (hundreds)
                            {
                                return UnityEngine.Random.Range(1, 9 + 1) * 100;
                            }
                            else
                            {
                                return UnityEngine.Random.Range(0, 999 + 1);
                            }

                        case Levels.Level_6:
                            return UnityEngine.Random.Range(0, 20 + 1);

                        case Levels.Level_7:
                            return UnityEngine.Random.Range(0, 100 + 1);

                        case Levels.Level_8:
                            return UnityEngine.Random.Range(0, 100 + 1);

                        case Levels.Level_9:
                            return UnityEngine.Random.Range(0, 100 + 1);

                        case Levels.Level_10:
                            return UnityEngine.Random.Range(0, 100 + 1);
                    }

                    break;
            }

            return 0;
        }
    }
}
