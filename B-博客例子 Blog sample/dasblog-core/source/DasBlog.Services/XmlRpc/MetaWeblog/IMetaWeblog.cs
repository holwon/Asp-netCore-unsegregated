#region Copyright (c) 2003, newtelligence AG. All rights reserved.
/*
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
//
*/
#endregion
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasBlog.Services.XmlRpc.MetaWeblog
{
    public interface IMetaWeblog
    {
        /// <summary>
        /// Updates an existing post to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        [XmlRpcMethod("metaWeblog.editPost", Description = "Updates an existing post to a designated blog "
             + "using the metaWeblog API. Returns true if completed.")]
        bool metaweblog_editPost(
            string postid,
            string username,
            string password,
            Post post,
            bool publish);

        /// <summary>
        ///  Retrieves a list of valid categories for a post 
        /// using the metaWeblog API. Returns the metaWeblog categories
        /// struct collection.
        /// </summary>
        [XmlRpcMethod("metaWeblog.getCategories",
             Description = "Retrieves a list of valid categories for a post "
             + "using the metaWeblog API. Returns the metaWeblog categories "
             + "struct collection.")]
        CategoryInfo[] metaweblog_getCategories(
            string blogid,
            string username,
            string password);


        [XmlRpcMethod("metaWeblog.getPost",
             Description = "Retrieves an existing post using the metaWeblog "
             + "API. Returns the metaWeblog struct.")]
        Post metaweblog_getPost(
            string postid,
            string username,
            string password);

        [XmlRpcMethod("metaWeblog.getRecentPosts",
             Description = "Retrieves a list of the most recent existing post "
             + "using the metaWeblog API. Returns the metaWeblog struct collection.")]
        Post[] metaweblog_getRecentPosts(
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        [XmlRpcMethod("metaWeblog.newPost",
             Description = "Makes a new post to a designated blog using the "
             + "metaWeblog API. Returns postid as a string.")]
        string metaweblog_newPost(
            string blogid,
            string username,
            string password,
            Post post,
            bool publish);

        /// <summary>
        /// newMediaObject implementation : Xas
        /// </summary>
        [XmlRpcMethod("metaWeblog.newMediaObject",
             Description = "Upload a new file to the binary content. Returns url as a string")]
        UrlInfo metaweblog_newMediaObject(object blogid, string username, string password, MediaType enc);
    }
}
