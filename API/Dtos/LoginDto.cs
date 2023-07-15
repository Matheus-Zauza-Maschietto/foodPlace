using System;
using System.Diagnostics.Contracts;
using API.Models;
using Flunt.Notifications;
using Flunt.Validations;

namespace API.Dtos;

public class LoginDto : Notificador<LoginDto>
{
    public string Email { get; set; }
    public string Senha { get; set; }


    public override void Validar()
    {
        Contrato
            .IsEmail(Email, "email")
            .IsNotNullOrEmpty(Senha, "senha");

        base.Validar();
    }
}
