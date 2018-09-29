﻿using SoloLearn.Chat.Core.Entities;
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


        public override void Add(User entity)
        {
            entity.PasswordHash = HashMd5(entity.PasswordHash);

            if (GetSingleFirst(e => e.UserName == entity.UserName) == null)
            {
                base.Add(entity);
            }
            else
                throw new Exception("User already exists!");

        }

        public User Authenticate(User user)
        {
            var password = HashMd5(user.PasswordHash);

            return GetSingleFirst(e => e.Email == user.Email && e.PasswordHash == password);
        }

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