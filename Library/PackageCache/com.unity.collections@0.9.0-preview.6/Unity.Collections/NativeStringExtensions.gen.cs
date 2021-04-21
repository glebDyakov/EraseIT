//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     TextTransform Samples/Packages/com.unity.collections/Unity.Collections/FixedStringExtensions.tt
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
    public static class NativeStringExtensions
    {
        public static int Append<T>(this ref T dest, byte src)
            where T : unmanaged, INativeList<byte>
        {
            if (dest.Length == dest.Capacity)
                return -1;
            dest[dest.Length++] = src;
            return 0;
        }

        public static int Append<T, U>(this ref T dest, in U src)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < src.Length; ++i)
            {
                var error = dest.Append(src[i]);
                if (error != 0)
                    return error;
            }

            return 0;
        }

        public static void Format<T, U, T0>(this ref T dest, in U format, in T0 arg0)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1>(this ref T dest, in U format, in T0 arg0, in T1 arg1)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4, T5>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
            where T5 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            case 5: dest.Append(arg5); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4, T5, T6>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
            where T5 : unmanaged, INativeList<byte>
            where T6 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            case 5: dest.Append(arg5); i += 2; break;
                            case 6: dest.Append(arg6); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4, T5, T6, T7>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
            where T5 : unmanaged, INativeList<byte>
            where T6 : unmanaged, INativeList<byte>
            where T7 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            case 5: dest.Append(arg5); i += 2; break;
                            case 6: dest.Append(arg6); i += 2; break;
                            case 7: dest.Append(arg7); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
            where T5 : unmanaged, INativeList<byte>
            where T6 : unmanaged, INativeList<byte>
            where T7 : unmanaged, INativeList<byte>
            where T8 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            case 5: dest.Append(arg5); i += 2; break;
                            case 6: dest.Append(arg6); i += 2; break;
                            case 7: dest.Append(arg7); i += 2; break;
                            case 8: dest.Append(arg8); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }

        public static void Format<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9)
            where T : unmanaged, INativeList<byte>
            where U : unmanaged, INativeList<byte>
            where T0 : unmanaged, INativeList<byte>
            where T1 : unmanaged, INativeList<byte>
            where T2 : unmanaged, INativeList<byte>
            where T3 : unmanaged, INativeList<byte>
            where T4 : unmanaged, INativeList<byte>
            where T5 : unmanaged, INativeList<byte>
            where T6 : unmanaged, INativeList<byte>
            where T7 : unmanaged, INativeList<byte>
            where T8 : unmanaged, INativeList<byte>
            where T9 : unmanaged, INativeList<byte>
        {
            for (var i = 0; i < format.Length; ++i)
            {
                if (format[i] == '{')
                {
                    if (format.Length - i >= 3 && format[i + 1] != '{')
                    {
                        var index = format[i + 1] - '0';
                        switch (index)
                        {
                            case 0: dest.Append(arg0); i += 2; break;
                            case 1: dest.Append(arg1); i += 2; break;
                            case 2: dest.Append(arg2); i += 2; break;
                            case 3: dest.Append(arg3); i += 2; break;
                            case 4: dest.Append(arg4); i += 2; break;
                            case 5: dest.Append(arg5); i += 2; break;
                            case 6: dest.Append(arg6); i += 2; break;
                            case 7: dest.Append(arg7); i += 2; break;
                            case 8: dest.Append(arg8); i += 2; break;
                            case 9: dest.Append(arg9); i += 2; break;
                            default:
                                dest.Append(format[i]);
                                break;
                        }
                    }
                }
                else
                    dest.Append(format[i]);
            }
        }
    }
}