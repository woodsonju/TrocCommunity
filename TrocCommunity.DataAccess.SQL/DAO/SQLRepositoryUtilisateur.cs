﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;
using System.Data.Entity;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryUtilisateur : SQLRepository<Utilisateur>
    {
        

        public SQLRepositoryUtilisateur(MyContext DataContext) : base(DataContext)
        {
            
        }


        public Utilisateur findByEmail(string email)
        {
/*            Utilisateur utilisateur = (from u in dataContext.Utilisateurs
                                      where u.Email.Contains(email)
                                      select u).SingleOrDefault();*/

            Utilisateur ut = (Utilisateur)dataContext
                                                    .Utilisateurs.Include(u => u.Adresse)
                                                    .SingleOrDefault(Utilisateur => Utilisateur.Email == email);

            return ut;

        }

        public Utilisateur findByUserName(string userName)
        {

            Utilisateur ut = (Utilisateur)dataContext
                                                    .Utilisateurs.Include(u => u.Adresse)
                                                    .SingleOrDefault(Utilisateur => Utilisateur.UserName == userName);

            return ut;

        }



        public Utilisateur FindByMailWithAdressId(string mail, int adresseId)
        {
            Utilisateur utilisateur = (Utilisateur)dataContext.Utilisateurs.Include(u => u.Adresse)
                                                .SingleOrDefault(u => u.Email == mail);
            return utilisateur;
            
        }

    /*    public Utilisateur UpdateUtilisateur(Utilisateur utilisateur)
        {
            dataContext.Utilisateurs.Include(u => u.Adresse)
                        .SingleOrDefault()
        }*/
/*
        public Client FindByAdresse()
        {
            Client client = (Client)(from cli in dataContext.Clients.AsNoTracking()
                                     join adr in dataContext.Adresses on cli.Adresse.Id equals adr.Id
                                     select cli);
            return client;
        }*/

    }
}
