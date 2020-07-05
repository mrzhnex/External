using System;
using System.Collections.Generic;

namespace External
{
    internal class ExInt
    {
        private List<byte> Values { get; set; }
        private bool Positive { get; set; }
        private ExInt(string value)
        {
            SetToZero();
            if (value != null && value != string.Empty && value.Length > 0)
                Add(GetBytesFromString(value));
        }
        private ExInt(ExInt old)
        {
            SetToZero();
            Add(old.Values);
        }
        
        #region public methods
        public override string ToString()
        {
            return ToString(Values);
        }
        public ExInt Plus(ExInt exInt)
        {
            if (Positive && exInt.Positive)
            {
                Add(exInt.Values);
            }
            else if (Positive && !exInt.Positive)
            {
                if (IsLargeThan(exInt))
                {
                    Remove(exInt.Values);
                }
                else
                {
                    return exInt.Plus(this);
                }
            }
            else if (!Positive && exInt.Positive)
            {
                if (IsLargeThan(exInt))
                {
                    Remove(exInt.Values);
                }
                else
                {
                    return exInt.Plus(this);
                }
            }
            else if (!Positive && !exInt.Positive)
            {
                Add(exInt.Values);
            }
            return this;
        }
        public ExInt Minus(ExInt exInt)
        {
            if (Positive && exInt.Positive)
            {
                if (IsLargeThan(exInt))
                {
                    Remove(exInt.Values);
                }
                else
                {
                    exInt.Remove(Values);
                    exInt.SwitchPositive();
                    return exInt;
                }
            }
            else if (Positive && !exInt.Positive)
            {
                Add(exInt.Values);
            }
            else if (!Positive && exInt.Positive)
            {
                Add(exInt.Values);
            }
            else if (!Positive && !exInt.Positive)
            {
                if (IsLargeThan(exInt))
                {
                    Remove(exInt.Values);
                }
                else
                {
                    exInt.Remove(Values);
                    exInt.SwitchPositive();
                    return exInt;
                }
            }
            return this;
        }
        #endregion

        #region private methods
        private void Add(List<byte> values)
        {
            values = CompareToOriginal(values);
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
            ClearEmpty();
        }
        private void Remove(List<byte> values)
        {
            values = CompareToOriginal(values);
            for (int i = 0; i < values.Count; i++)
            {
                if (Values[i] < values[i])
                {
                    if (i == values.Count - 1)
                    {
                        Values[i] = 0;
                    }
                    else
                    {
                        Values[i] = (byte)(10 - (values[i] - Values[i]));
                        values[i + 1]++;
                    }
                }
                else
                {
                    Values[i] -= values[i];
                }
            }
            ClearEmpty();
        }
        #endregion

        #region help methods
        private void SwitchPositive()
        {
            Positive = !Positive;
        }
        private void SetToZero()
        {
            Positive = true;
            Values = new List<byte> { 0 };
        }
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
        public bool IsEqual(ExInt exInt)
        {
            if (Values.Count != exInt.Values.Count)
                return false;
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (Values[i] != exInt.Values[i])
                    return false;
            }
            return true;
        }
        public bool IsLargeThan(ExInt exInt)
        {
            if (Values.Count > exInt.Values.Count)
                return true;
            if (Values.Count < exInt.Values.Count)
                return false;
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (Values[i] > exInt.Values[i])
                    return true;
                if (Values[i] < exInt.Values[i])
                    return false;
            }
            return false;
        }
        public bool IsLargeThanForReal(ExInt exInt)
        {
            if (Positive && exInt.Positive)
                return IsLargeThan(exInt);
            if (Positive && !exInt.Positive)
                return true;
            if (!Positive && exInt.Positive)
                return false;
            if (!Positive && !exInt.Positive && IsEqual(exInt))
                return false;
            if (!Positive && !exInt.Positive)
                return !IsLargeThan(exInt);
            return false;
        }
        private string GetPositive()
        {
            return Positive ? string.Empty : "-";
        }
        private string ToString(List<byte> values)
        {
            string result = GetPositive();
            for (int i = values.Count - 1; i >= 0; i--)
            {
                result += values[i];
            }
            return result;
        }
        private List<byte> GetBytesFromString(string value)
        {
            List<byte> tempValues = new List<byte>();
            for (int i = value.Length - 1; i >= 0; i--)
            {
                tempValues.Add(GetByte(value, i));
            }
            return tempValues;
        }
        private List<byte> CompareToOriginal(List<byte> values)
        {
            if (values.Count > Values.Count)
            {
                int count = values.Count - Values.Count;
                for (int i = 0; i < count; i++)
                {
                    Values.Add(0);
                }
            }
            else if (values.Count < Values.Count)
            {
                int count = Values.Count - values.Count;
                for (int i = 0; i < count; i++)
                {
                    values.Add(0);
                }
            }
            return values;
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
        public static implicit operator ExInt(string v)
        {
            return new ExInt(v);
        }
        public static implicit operator ExInt(ulong v)
        {
            return new ExInt(v.ToString());
        }
        public static ExInt operator +(ExInt exInt, string v)
        {
            return exInt + new ExInt(v);
        }
        public static ExInt operator -(ExInt exInt, string v)
        {
            return exInt - new ExInt(v);
        }
        public static ExInt operator +(ExInt left, ExInt right)
        {
            return left.Plus(right);
        }
        public static ExInt operator -(ExInt left, ExInt right)
        {
            return left.Minus(right);
        }
        public static ExInt operator ++(ExInt exInt)
        {
            return exInt.Plus(1);
        }
        public static ExInt operator --(ExInt exInt)
        {
            return exInt.Minus(1);
        }
        public static bool operator <(ExInt left, ExInt right)
        {
            return right.IsLargeThanForReal(left);
        }
        public static bool operator >(ExInt left, ExInt right)
        {
            return left.IsLargeThanForReal(right);
        }
        public static ExInt operator *(ExInt exInt, string v)
        {
            ExInt tempExInt = new ExInt(exInt);
            ExInt maxCount = new ExInt(v);
            for (ExInt count = 1; count < maxCount; count++)
            {
                exInt.Plus(tempExInt);
            }
            return exInt;
        }
        #endregion
    }
}