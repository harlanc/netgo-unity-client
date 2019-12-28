using System;
using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace Netgo.Library
{
    public class NGProtobufConverter<T>  
    {
        public static List<T> RepeatedField2List(RepeatedField<T> rf)
        {
            List<T> rv = new List<T>();
            foreach(var val in rf)
            {
                rv.Add(val);
            }
            return rv;
        }

        public static T[] RepeatedField2Array(RepeatedField<T> rf)
        {
            T[] rv = new T[rf.Count];
            for (int idx = 0; idx < rf.Count; idx++)
            {
                rv[idx] = rf[idx];
            }
            return rv;
        }

        public static RepeatedField<T> Array2RepeatedField(T [] objs)
        {
            RepeatedField<T> rv = new RepeatedField<T>();
            foreach(var value in objs)
            {
                rv.Add(value);
            }
            return rv;
        }
        
    }
}
