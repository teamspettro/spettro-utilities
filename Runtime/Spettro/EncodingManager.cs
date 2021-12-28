// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using System.Xml;
// using System.Xml.Serialization;
// using System;
// using System.Runtime.Serialization.Formatters.Binary;
// using System.Text;
// using System.Text.RegularExpressions;

// namespace Spettro
// {
//     public static class EncodingManager
//     {
//         public static string EncodeB64(object objectToEncode)
//         {
//             string b64 = Convert.ToBase64String(ObjectToByteArray(objectToEncode));
//             return b64;
//         }
//         public static object DecodeB64(string objectToDecode)
//         {
//             byte[] c = Convert.FromBase64String(objectToDecode);

//             return ByteArrayToObject<object>(c);
//         }
//         #region https://stackoverflow.com/questions/33022660/how-to-convert-byte-array-to-any-type
//         // Convert an object to a byte array
//         public static byte[] ObjectToByteArray(object obj)
//         {
//             if (obj == null)
//                 return null;
//             BinaryFormatter bf = new BinaryFormatter();
//             using (MemoryStream ms = new MemoryStream())
//             {
//                 bf.Serialize(ms, obj);
//                 return ms.ToArray();
//             }
//         }
//         public static T ByteArrayToObject<T>(byte[] arrBytes)
//         {
//             try
//             {
//                 if (arrBytes == null)
//                     return default(T);
//                 BinaryFormatter bf = new BinaryFormatter();
//                 using (MemoryStream ms = new MemoryStream(arrBytes))
//                 {
//                     object obj = bf.Deserialize(ms);
//                     return (T)obj;
//                 }
//             }
//             catch
//             {
//                 Debug.LogError("Couldn't decode byte array to object.");
//                 return default(T);
//             }
//         }
//         #endregion
//         public static bool IsBase64String(this string s)
//         {
//             s = s.Trim();
//             return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

//         }
//     }
// }

