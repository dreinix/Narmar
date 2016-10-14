using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NARMAR
{
    public class ValidationAttributes
    {
        public class ListMinLength : ValidationAttribute
        {
            private int _length;
            public ListMinLength(int length)
            {
                _length = length;
            }
            public override bool IsValid(object value)
            {
                var list = value as IList;
                if (list != null)
                {
                    return list.Count >= _length;
                }
                return false;
            }
        }
        public class ListMaxLength : ValidationAttribute
        {
            private int _length;
            public ListMaxLength(int length)
            {
                _length = length;
            }
            public override bool IsValid(object value)
            {
                var list = value as IList;
                if (list != null)
                {
                    return list.Count <= _length;
                }
                return false;
            }
        }
    }
}
