/*  Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security;
using System.Text;
using StreamReader = System.IO.StreamReader;

namespace Alphaleonis.Win32.Filesystem
{
   public static partial class File
   {
      #region ReadAllText

      /// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
      /// <param name="path">The file to open for reading.</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllText(string path)
      {
         return ReadAllTextCore(null, path, NativeMethods.DefaultFileEncoding, PathFormat.RelativePath);
      }

      /// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The <see cref="Encoding"/> applied to the contents of the file.</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllText(string path, Encoding encoding)
      {
         return ReadAllTextCore(null, path, encoding, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Opens a text file, reads all lines of the file, and then closes the file.</summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllText(string path, PathFormat pathFormat)
      {
         return ReadAllTextCore(null, path, NativeMethods.DefaultFileEncoding, pathFormat);
      }

      /// <summary>[AlphaFS] Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The <see cref="Encoding"/> applied to the contents of the file.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllText(string path, Encoding encoding, PathFormat pathFormat)
      {
         return ReadAllTextCore(null, path, encoding, pathFormat);
      }

      #region Transactional

      /// <summary>[AlphaFS] Opens a text file, reads all lines of the file, and then closes the file.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The file to open for reading.</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllTextTransacted(KernelTransaction transaction, string path)
      {
         return ReadAllTextCore(transaction, path, NativeMethods.DefaultFileEncoding, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The <see cref="Encoding"/> applied to the contents of the file.</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllTextTransacted(KernelTransaction transaction, string path, Encoding encoding)
      {
         return ReadAllTextCore(transaction, path, encoding, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Opens a text file, reads all lines of the file, and then closes the file.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllTextTransacted(KernelTransaction transaction, string path, PathFormat pathFormat)
      {
         return ReadAllTextCore(transaction, path, NativeMethods.DefaultFileEncoding, pathFormat);
      }

      /// <summary>[AlphaFS] Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The <see cref="Encoding"/> applied to the contents of the file.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      /// <returns>All lines of the file.</returns>
      [SecurityCritical]
      public static string ReadAllTextTransacted(KernelTransaction transaction, string path, Encoding encoding, PathFormat pathFormat)
      {
         return ReadAllTextCore(transaction, path, encoding, pathFormat);
      }

      #endregion // Transacted

      #endregion // ReadAllText

      #region Internal Methods

      /// <summary>Open a file, read all lines of the file with the specified encoding, and then close the file.</summary>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The <see cref="Encoding"/> applied to the contents of the file.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>
      /// <returns>All lines of the file.</returns>
      [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
      [SecurityCritical]
      internal static string ReadAllTextCore(KernelTransaction transaction, string path, Encoding encoding, PathFormat pathFormat)
      {
         using (StreamReader sr = new StreamReader(OpenCore(transaction, path, FileMode.Open, FileAccess.Read, FileShare.Read, ExtendedFileAttributes.SequentialScan, null, null, pathFormat), encoding))
            return sr.ReadToEnd();
      }

      #endregion // Internal Methods

   }
}
