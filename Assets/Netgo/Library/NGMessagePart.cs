using System;
using Google.Protobuf.WellKnownTypes;
using pb = global::Google.Protobuf;
using UnityEngine;

namespace Netgo.Library
{
    public sealed partial class NGVector3
    {
        NGVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Automatic conversion from NGVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector3(NGVector3 rValue)
        {
            return new Vector3(rValue.X, rValue.Y, rValue.Z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to NGVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator NGVector3(Vector3 rValue)
        {
            return new NGVector3(rValue.x, rValue.y, rValue.z);
        }
    }

    public sealed partial class NGQuaternion
    {
        NGQuaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            X = z;
            W = w;
        }
        /// <summary>
        /// Automatic conversion from NGQuaternion to Quaternion
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Quaternion(NGQuaternion rValue)
        {
            return new Quaternion(rValue.X, rValue.Y, rValue.Z, rValue.W);
        }

        /// <summary>
        /// Automatic conversion from Quaternion to NGQuaternion
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator NGQuaternion(Quaternion rValue)
        {
            return new NGQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
        }

    }

    public sealed partial class NGColor
    {
        NGColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        /// <summary>
        /// Automatic conversion from NGQuaternion to Quaternion
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Color(NGColor rValue)
        {
            return new Color(rValue.R, rValue.G, rValue.B, rValue.A);
        }

        /// <summary>
        /// Automatic conversion from Quaternion to NGQuaternion
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator NGColor(Color rValue)
        {
            return new NGColor(rValue.r, rValue.g, rValue.b, rValue.a);
        }

    }



    public sealed partial class NGAny
    {

        public NGAny(double val)
        {
            this.NgDouble = val;
        }
        public NGAny(float val)
        {
            this.NgFloat = val;
        }
        public NGAny(Int32 val)
        {
            this.NgInt32 = val;
        }
        public NGAny(Int64 val)
        {
            this.NgInt64 = val;
        }

        public NGAny(UInt32 val)
        {
            this.NgUint32 = val;
        }
        public NGAny(UInt64 val)
        {
            this.NgUint64 = val;
        }

        public NGAny(bool val)
        {
            this.NgBool = val;
        }
        public NGAny(string val)
        {
            this.NgString = val;
        }
        public NGAny(pb::ByteString val)
        {
            this.NgBytes = val;
        }
        public NGAny(Vector3 val)
        {
            this.NgVector3 = val;
        }

        public NGAny(Quaternion val)
        {
            this.NgQuaternion = val;
        }

        public NGAny(Color color)
        {
            this.NgColor = color;
        }


        public static implicit operator NGAny(double obj)
        {
            return new NGAny(obj);
        }
        public static implicit operator double?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgDouble)
            {
                NGLogger.LogError("NGAny cannot be casted to double.");
                return null;
            }
            return obj.NgDouble;
        }
        public static implicit operator NGAny(float obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator float?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgFloat)
            {
                NGLogger.LogError("NGAny cannot be casted to float.");
                return null;
            }
            return obj.NgFloat;
        }
        public static implicit operator NGAny(Int32 obj)
        {
            return new NGAny(obj);
        }
        public static implicit operator Int32?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgInt32)
            {
                NGLogger.LogError("NGAny cannot be casted to Int32.");
                return null;
            }
            return obj.NgInt32;
        }

        public static implicit operator NGAny(Int64 obj)
        {
            return new NGAny(obj);
        }
        public static implicit operator Int64?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgInt64)
            {
                NGLogger.LogError("NGAny cannot be casted to Int64.");
                return null;
            }
            return obj.NgInt64;
        }

        public static implicit operator NGAny(UInt32 obj)
        {
            return new NGAny(obj);
        }


        public static implicit operator UInt32?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgUint32)
            {
                NGLogger.LogError("NGAny cannot be casted to UInt32.");
                return null;
            }
            return obj.NgUint32;
        }
        public static implicit operator NGAny(UInt64 obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator UInt64?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgUint64)
            {
                NGLogger.LogError("NGAny cannot be casted to UInt64.");
                return null;
            }
            return obj.NgUint64;
        }

        public static implicit operator NGAny(bool obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator bool?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgBool)
            {
                NGLogger.LogError("NGAny cannot be casted to bool.");
                return null;
            }
            return obj.NgBool;
        }

        public static implicit operator NGAny(string obj)
        {
            return new NGAny(obj);
        }


        public static implicit operator string(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgString)
            {
                NGLogger.LogError("NGAny cannot be casted to string.");
                return null;
            }
            return obj.NgString;
        }


        public static implicit operator NGAny(pb::ByteString obj)
        {
            return new NGAny(obj);
        }


        public static implicit operator pb::ByteString(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgBytes)
            {
                NGLogger.LogError("NGAny cannot be casted to ByteString.");
                return null;
            }
            return obj.NgBytes;
        }

        public static implicit operator NGAny(Vector3 obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator Vector3?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgVector3)
            {
                NGLogger.LogError("NGAny cannot be casted to Vector3.");
                return null;
            }
            return obj.NgVector3;
        }

        public static implicit operator NGAny(Quaternion obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator Quaternion?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgQuaternion)
            {
                NGLogger.LogError("NGAny cannot be casted to Quaternion.");
                return null;
            }
            return obj.NgQuaternion;
        }
        public static implicit operator NGAny(Color obj)
        {
            return new NGAny(obj);
        }

        public static implicit operator Color?(NGAny obj)
        {
            if (obj.NgTypeCase != NgTypeOneofCase.NgColor)
            {
                NGLogger.LogError("NGAny cannot be casted to Color.");
                return null;
            }
            return obj.NgColor;
        }

        // public static explicit operator NGAny(object  obj)
        // {
        //     if (obj is double)
        //     {
        //         return new NGAny((double)obj);
        //     }
        //     else if (obj is float)
        //     {
        //         return new NGAny((float)obj);
        //     }
        //     else if (obj is Int32)
        //     {
        //         return new NGAny((Int32)obj);
        //     }
        //     else if (obj is Int64)
        //     {
        //         return new NGAny((Int64)obj);
        //     }
        //     else if (obj is UInt32)
        //     {
        //         return new NGAny((UInt32)obj);
        //     }
        //     else if (obj is UInt64)
        //     {
        //         return new NGAny((UInt64)obj);
        //     }
        //     else if (obj is bool)
        //     {
        //         return new NGAny((bool)obj);
        //     }
        //     else if (obj is string)
        //     {
        //         return new NGAny((string)obj);
        //     }
        //     else if (obj is pb::ByteString)
        //     {
        //         return new NGAny((pb::ByteString)obj);
        //     }
        //     else if (obj is Vector3)
        //     {
        //         return new NGAny((Vector3)obj);
        //     }
        //     else if (obj is Quaternion)
        //     {
        //         return new NGAny((Quaternion)obj);
        //     }
        //     else
        //     {
        //         NGLogger.LogError("The type" + obj.GetType() + " is not supported");
        //         return null;
        //     }
        //  }


    }


}
