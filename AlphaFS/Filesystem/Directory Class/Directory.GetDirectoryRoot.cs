/* Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   partial class Directory
   {
      #region GetDirectoryRoot

      /// <summary>Returns the volume information, root information, or both for the specified path.</summary>
      /// <param name="path">The path of a file or directory.</param>
      /// <returns>The volume information, root information, or both for the specified path, or <see langword="null"/> if <paramref name="path"/> path does not contain root directory information.</returns>
      [SecurityCritical]
      public static string GetDirectoryRoot(string path)
      {
         return GetDirectoryRootInternal(null, path, PathFormat.Relative);
      }

      /// <summary>Returns the volume information, root information, or both for the specified path.</summary>
      /// <returns>The volume information, root information, or both for the specified path, or <see langword="null"/> if <paramref name="path"/> path does not contain root directory information.</returns>
      /// <param name="path">The path of a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      [SecurityCritical]
      public static string GetDirectoryRoot(string path, PathFormat pathFormat)
      {
         return GetDirectoryRootInternal(null, path, pathFormat);
      }

      /// <summary>Returns the volume information, root information, or both for the specified path.</summary>
      /// <returns>The volume information, root information, or both for the specified path, or <see langword="null"/> if <paramref name="path"/> path does not contain root directory information.</returns>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The path of a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      [SecurityCritical]
      public static string GetDirectoryRoot(KernelTransaction transaction, string path, PathFormat pathFormat)
      {
         return GetDirectoryRootInternal(transaction, path, pathFormat);
      }

      /// <summary>[AlphaFS] Returns the volume information, root information, or both for the specified path.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The path of a file or directory.</param>
      /// <returns>The volume information, root information, or both for the specified path, or <see langword="null"/> if <paramref name="path"/> path does not contain root directory information.</returns>
      [SecurityCritical]
      public static string GetDirectoryRoot(KernelTransaction transaction, string path)
      {
         return GetDirectoryRootInternal(transaction, path, PathFormat.Relative);
      }

      #endregion // GetDirectoryRoot

      #region Internal Methods

      /// <summary>[AlphaFS] Unified method GetDirectoryRootInternal() to return the volume information, root information, or both for the specified path.
      /// <returns>
      /// <para>Returns the volume information, root information, or both for the specified path,</para>
      /// <para> or <see langword="null"/> if <paramref name="path"/> path does not contain root directory information.</para>
      /// </returns>
      /// </summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The path of a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      [SecurityCritical]
      internal static string GetDirectoryRootInternal(KernelTransaction transaction, string path, PathFormat pathFormat)
      {
         string pathLp = Path.GetExtendedLengthPathInternal(transaction, path, pathFormat, GetFullPathOptions.CheckInvalidPathChars);
         pathLp = Path.GetRegularPathInternal(pathLp, false, false, false, false);
         string rootPath = Path.GetPathRoot(pathLp, false);

         return Utils.IsNullOrWhiteSpace(rootPath) ? null : rootPath;
      }

      #endregion // GetDirectoryRootInternal
   }
}
