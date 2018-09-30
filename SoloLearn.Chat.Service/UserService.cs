using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SoloLearn.Chat.Service
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository) { }

        /// <summary>
        /// Overrides the Add method to handle the password hashing
        /// </summary>
        /// <param name="entity"></param>
        public override void Add(User entity)
        {
            if (!string.IsNullOrEmpty(entity.Email) && !string.IsNullOrEmpty(entity.PasswordHash) && !string.IsNullOrEmpty(entity.UserName))
            {
                entity.PasswordHash = HashMd5(entity.PasswordHash);

                if (GetSingleFirst(e => e.UserName == entity.UserName) == null)
                {
                    base.Add(entity);
                }
                else
                    throw new Exception("User already exists!");

            }
        }

        /// <summary>
        /// Authenticates an user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>An authenticated User</returns>
        public User Authenticate(User user)
        {
            //hash the password
            var password = HashMd5(user.PasswordHash);
            
            return GetSingleFirst(e => e.Email == user.Email && e.PasswordHash == password);
        }

        /// <summary>
        /// Hashs a string to MD5
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns></returns>
        private string HashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
