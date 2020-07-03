using System;
using System.Collections.Generic;

namespace External
{
    internal class ExInt
    {
        private readonly List<byte> Values = new List<byte>();
        private bool Positive { get; set; }
        private ExInt(string value)
        {
            Positive = true;
            Add(GetBytesFromString(0.ToString()));
            Add(GetBytesFromString(value));
        }
        
        #region public methods
        public override string ToString()
        {
            string result = GetPositive();
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                result += Values[i];
            }
            return result;
        }
        #endregion

        #region private methods
        private void Add(List<byte> values)
        {
            if (Positive)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (Values[i] + values[i] > 9)
                    {
                        Values[i] = (byte)(Values[i] + values[i] - 10);
                        if (i == values.Count - 1)
                        {
                            Values.Add(1);
                        }
                        else
                        {
                            Values[i + 1] += 1;
                        }
                    }
                    else
                    {
                        Values[i] += values[i];
                    }
                }
            }
            else
            {
                if (IsEqual(values))
                {
                    Values.Clear();
                    Values.Add(0);
                    Positive = true;
                }
                else
                {
                    if (IsLargeThanOriginal(values))
                    {
                        for (int i = values.Count - 1; i >= 0; i--)
                        {
                            if (values[i] < Values[i])
                            {
                                values[i] = (byte)(10 - (Values[i] - values[i]));
                                if (i < values.Count - 1)
                                {
                                    values[i + 1] -= 1;
                                }
                            }
                            else
                            {
                                values[i] -= Values[i];
                            }
                        }
                        Values.Clear();
                        Values.AddRange(values);
                        Positive = true;
                    }
                    else
                    {
                        for (int i = values.Count - 1; i >= 0; i--)
                        {
                            if (Values[i] < values[i])
                            {
                                Values[i] = (byte)(10 - (values[i] - Values[i]));
                                if (i < values.Count - 1)
                                {
                                    Values[i + 1] -= 1;
                                }
                            }
                            else
                            {
                                Values[i] -= values[i];
                            }
                        }
                    }
                }               
            }
            ClearEmpty();
        }
        private void Remove(List<byte> values)
        {
            if (IsLargeThanOriginal(values))
            {
                Positive = false;
                for (int i = Values.Count - 1; i >= 0; i--)
                {
                    if (values[i] < Values[i])
                    {
                        values[i] = (byte)(10 - (Values[i] - values[i]));
                        if (i < values.Count - 1)
                        {
                            values[i + 1] -= 1;
                        }
                    }
                    else
                    {
                        values[i] -= Values[i];
                    }
                }
                Values.Clear();
                Values.AddRange(values);
            }
            else
            {
                if (Positive)
                {
                    if (IsEqual(values))
                    {
                        Values.Clear();
                        Values.Add(0);
                    }
                    else
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            if (Values[i] < values[i])
                            {
                                if (i < values.Count - 1)
                                {
                                    values[i + 1] = (byte)(10 - (10 - (values[i] - Values[i])));
                                }
                                Values[i] = (byte)(10 - (values[i] - Values[i]));
                                if (i == values.Count - 1)
                                {
                                    Values[i] = 0;
                                }
                            }
                            else
                            {
                                Values[i] -= values[i];
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (Values[i] + values[i] > 9)
                        {
                            Values[i] = (byte)(Values[i] + values[i] - 10);
                            if (i == values.Count - 1)
                            {
                                Values.Add(1);
                            }
                            else
                            {
                                Values[i + 1] += 1;
                            }
                        }
                        else
                        {
                            Values[i] += values[i];
                        }
                    }
                }
            }
            ClearEmpty();
        }
        #endregion

        #region help methods
        private void ClearEmpty()
        {
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (i == Values.Count - 1 && Values[i] == 0 && i != 0)
                    Values.RemoveAt(i);
                else
                    break;
            }
        }
        private bool IsEqual(List<byte> values)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                if (values[i] != Values[i])
                    return false;
            }
            return true;
        }
        public bool IsLargeThanOriginal(List<byte> values)
        {
            if (values.Count > Values.Count)
                return true;
            if (values.Count < Values.Count)
                return false;
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (values[i] > Values[i])
                    return true;
                if (values[i] < Values[i])
                    return false;
            }
            return false;
        }
        private string GetPositive()
        {
            return Positive ? string.Empty : "-";
        }
        private List<byte> GetBytesFromString(string value)
        {
            List<byte> tempValues = new List<byte>();
            for (int i = value.Length - 1; i >= 0; i--)
            {
                tempValues.Add(GetByte(value, i));
            }
            if (tempValues.Count > Values.Count)
            {
                int count = tempValues.Count - Values.Count;
                for (int i = 0; i < count; i++)
                {
                    Values.Add(0);
                }
            }
            else if (tempValues.Count < Values.Count)
            {
                int count = Values.Count - tempValues.Count;
                for (int i = 0; i < count; i++)
                {
                    tempValues.Add(0);
                }
            }
            return tempValues;
        }
        private byte GetByte(string value, int index)
        {
            if (byte.TryParse(value.ToCharArray()[index].ToString(), out byte result))
                return result;
            else
                return 0;
        }
        #endregion

        #region operators
        public static implicit operator ExInt(ulong v)
        {
            return new ExInt(v.ToString());
        }
        public static implicit operator ExInt(string v)
        {
            return new ExInt(v);
        }
        public static ExInt operator +(ExInt exInt, string v)
        {
            if (v != null && v.Length > 0)
                exInt.Add(exInt.GetBytesFromString(v));
            return exInt;
        }
        public static ExInt operator -(ExInt exInt, string v)
        {
            if (v != null && v.Length > 0)
                exInt.Remove(exInt.GetBytesFromString(v));
            return exInt;
        }
        public static ExInt operator ++(ExInt exInt)
        {
            exInt.Add(new List<byte>() { 1 });
            return exInt;
        }
        public static ExInt operator --(ExInt exInt)
        {
            exInt.Remove(new List<byte>() { 1 });
            return exInt;
        }
        public static bool operator <(ExInt left, ExInt right)
        {
            if (left.IsLargeThanOriginal(right.Values))
                return true;
            return false;
        }
        public static bool operator >(ExInt left, ExInt right)
        {
            if (right.IsLargeThanOriginal(left.Values))
                return true;
            return false;
        }
        public static ExInt operator *(ExInt exInt, string v)
        {
            if (v != null && v.Length > 0)
            {
                List<byte> tempValues = new List<byte>();
                tempValues.AddRange(exInt.Values);
                ExInt maxValue = v;
                for (ExInt count = 1; count < maxValue; count++)
                {
                    Console.WriteLine("Add " + tempValues[0] + " to " + exInt + " in step " + count + " max " + maxValue);
                    exInt.Add(tempValues);
                }
            }
            return exInt;
        }
        #endregion
    }
}