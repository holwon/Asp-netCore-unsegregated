#region Copyright (c) 2003, newtelligence AG. All rights reserved.
// Copyright (c) 2003, newtelligence AG. (http://www.newtelligence.com)
// Original BlogX Source Code: Copyright (c) 2003, Chris Anderson (http://simplegeek.com)
// All rights reserved.
//  
// Redistribution and use in source and binary forms, with or without modification, are permitted 
// provided that the following conditions are met: 
//  
// (1) Redistributions of source code must retain the above copyright notice, this list of 
// conditions and the following disclaimer. 
// (2) Redistributions in binary form must reproduce the above copyright notice, this list of 
// conditions and the following disclaimer in the documentation and/or other materials 
// provided with the distribution. 
// (3) Neither the name of the newtelligence AG nor the names of its contributors may be used 
// to endorse or promote products derived from this software without specific prior 
// written permission.
//      
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS 
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER 
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// -------------------------------------------------------------------------
//
// Original BlogX source code (c) 2003 by Chris Anderson (http://simplegeek.com)
// 
// newtelligence is a registered trademark of newtelligence Aktiengesellschaft.
// 
// For portions of this software, the some additional copyright notices may apply 
// which can either be found in the license.txt file included in the source distribution
// or following this notice. 
// -------
// Copyright 2003, Microsoft Coporation
//
// Original source code by Nikhil Kothari
// 
// Integrated into DasBlog by Chris Anderson
//
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
// -------
#endregion

namespace newtelligence.DasBlog.Runtime.Util.Html
{
    public sealed class HtmlFormatterOptions
    {
        public HtmlFormatterOptions(char indentChar, int indentSize, int maxLineLength, bool makeXhtml)
            : this(indentChar, indentSize, maxLineLength, HtmlFormatterCase.LowerCase, HtmlFormatterCase.LowerCase, makeXhtml)
        {
        }

        public HtmlFormatterOptions(char indentChar, int indentSize, int maxLineLength, HtmlFormatterCase elementCasing, HtmlFormatterCase attributeCasing, bool makeXhtml)
        {
            IndentChar = indentChar;
            IndentSize = indentSize;
            MaxLineLength = maxLineLength;
            ElementCasing = elementCasing;
            AttributeCasing = attributeCasing;
            MakeXhtml = makeXhtml;
        }

        public HtmlFormatterCase AttributeCasing
        {
            get;
            private set;
        }

        public HtmlFormatterCase ElementCasing
        {
            get;
            private set;
        }

        public char IndentChar
        {
            get;
            private set;
        }

        public int IndentSize
        {
            get;
            private set;
        }

        public bool MakeXhtml
        {
            get;
            private set;
        }

        public int MaxLineLength
        {
            get;
            private set;
        }
    }
}
