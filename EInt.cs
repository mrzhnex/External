﻿using System.Collections.Generic;

namespace External
{
    internal class EInt
    {
        #region fields
        private List<byte> Values { get; set; }
        private bool Positive { get; set; }
        #endregion

        #region constructors
        private EInt(string value, bool positive = true)
        {
            SetToZero();
            Positive = positive;
            if (value != null && value != string.Empty && value.Length > 0)
                Add(GetBytesFromString(value));
        }
        private EInt(EInt old)
        {
            SetToZero();
            Add(old.Values);
        }
        #endregion

        #region public methods
        public override string ToString()
        {
            return GetPositive() + ToString(Values);
        }
        public void Replace(ref EInt right)
        { 
            EInt temp = right.Clone();
            right.Values = Values;
            Values = temp.Values;
        }
        public EInt Clone()
        {
            return new EInt(this);
        }
        public EInt Plus(EInt another)
        {
            if (Positive && another.Positive)
            {
                Add(another.Values);
            }
            else if (Positive && !another.Positive)
            {
                if (IsLargeThan(another))
                {
                    Remove(another.Values);
                }
                else if (IsEqual(another))
                {
                    SetToZero();
                }
                else
                {
                    return another.Plus(this);
                }
            }
            else if (!Positive && another.Positive)
            {
                if (IsLargeThan(another))
                {
                    Remove(another.Values);
                }
                else if (IsEqual(another))
                {
                    SetToZero();
                }
                else
                {
                    return another.Plus(this);
                }
            }
            else if (!Positive && !another.Positive)
            {
                Add(another.Values);
            }
            return this;
        }
        public EInt Minus(EInt another)
        {
            if (Positive && another.Positive)
            {
                if (IsLargeThan(another))
                {
                    Remove(another.Values);
                }
                else if (IsEqual(another))
                {
                    SetToZero();
                }
                else
                {
                    another.Remove(Values);
                    another.SetPositive(false);
                    return another;
                }
            }
            else if (Positive && !another.Positive)
            {
                Add(another.Values);
            }
            else if (!Positive && another.Positive)
            {
                Add(another.Values);
            }
            else if (!Positive && !another.Positive)
            {
                if (IsLargeThan(another))
                {
                    Remove(another.Values);
                }
                else if (IsEqual(another))
                {
                    SetToZero();
                }
                else
                {
                    another.Remove(Values);
                    another.SetPositive(true);
                    return another;
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

        #region helper methods
        private void SetPositive(bool Positive)
        {
            this.Positive = Positive;
        }
        private void SetToZero()
        {
            SetPositive(true);
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
        public bool IsEqual(EInt another)
        {
            if (Values.Count != another.Values.Count)
                return false;
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (Values[i] != another.Values[i])
                    return false;
            }
            return true;
        }
        public bool IsLargeThan(EInt another)
        {
            if (Values.Count > another.Values.Count)
                return true;
            if (Values.Count < another.Values.Count)
                return false;
            for (int i = Values.Count - 1; i >= 0; i--)
            {
                if (Values[i] > another.Values[i])
                    return true;
                if (Values[i] < another.Values[i])
                    return false;
            }
            return false;
        }
        public bool IsLargeThanForReal(EInt another)
        {
            if (Positive && another.Positive)
                return IsLargeThan(another);
            if (Positive && !another.Positive)
                return true;
            if (!Positive && another.Positive)
                return false;
            if (!Positive && !another.Positive && IsEqual(another))
                return false;
            if (!Positive && !another.Positive)
                return !IsLargeThan(another);
            return false;
        }
        private string GetPositive()
        {
            return Positive ? string.Empty : "-";
        }
        private string ToString(List<byte> values)
        {
            string result = string.Empty;
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
        public static implicit operator EInt(string v)
        {
            return new EInt(v);
        }
        public static implicit operator EInt(long v)
        {
            return new EInt(v.ToString(), v >= 0);
        }
        public static explicit operator string(EInt v)
        {
            return v.ToString();
        }
        public static EInt operator +(EInt left, EInt right)
        {
            return left.Plus(right);
        }
        public static EInt operator -(EInt left, EInt right)
        {
            return left.Minus(right);
        }
        public static EInt operator *(EInt left, EInt right)
        {
            if (right == 0)
            {
                left.SetToZero();
            }
            else
            {
                if (right.IsLargeThan(left))
                    left.Replace(ref right);
                if (left.Positive && !right.Positive)
                {
                    left.SetPositive(false);
                    right.SetPositive(true);
                }
                else if (!left.Positive && !right.Positive)
                {
                    left.SetPositive(true);
                    right.SetPositive(true);
                }
                EInt tempExInt = left.Clone();
                for (EInt count = 1; count < right; count++)
                    left.Add(tempExInt.Values);
            }
            return left;
        }
        public static EInt operator /(EInt left, EInt right)
        {
            return 0;
        }
        public static EInt operator ++(EInt left)
        {
            return left.Plus(1);
        }
        public static EInt operator --(EInt left)
        {
            return left.Minus(1);
        }
        public static bool operator <(EInt left, EInt right)
        {
            return right.IsLargeThanForReal(left);
        }
        public static bool operator >(EInt left, EInt right)
        {
            return left.IsLargeThanForReal(right);
        }
        public static bool operator ==(EInt left, EInt right)
        {
            return left.Positive == right.Positive && left.IsEqual(right);
        }
        public static bool operator !=(EInt left, EInt right)
        {
            return left.Positive != right.Positive || !left.IsEqual(right);
        }
        #endregion
    }
}