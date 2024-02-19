﻿namespace EsportsProfileWebApi.Web.Orchestrators;

using EsportsProfileWebApi.INFRASTRUCTURE;
using EsportsProfileWebApi.Web.Helpers;
using EsportsProfileWebApi.Web.Repository;
using EsportsProfileWebApi.Web.Requests.User;
using EsportsProfileWebApi.Web.Responses.User;
using System.Security.Claims;

public class UserOrchestrator : IUserOrchestrator
{
    private readonly IUserRepository _userRepository;
    private readonly IDataRepository _dataRepository;
    public UserOrchestrator(IUserRepository userRepository, IConfiguration config, IDataRepository dataRepository)
    {
        _userRepository = userRepository ?? throw new NotImplementedException();
        _dataRepository = dataRepository ?? throw new NotImplementedException();
    }

    public async Task<GetUserDataResponse> RegisterUser(RegisterRequest request)
    {
        // find if the user is valid, if they are create the claims or retrieve them from the db
        var alreadyExists = await _userRepository.CheckIfUserExists(request.Username,request.Email);
        if (alreadyExists)
            throw new Exception("User already exists.");

        // create the user and add them to the db
        var userData = await _dataRepository.CreateUserDataForUsername(request.Username)
                ?? throw new Exception("Error creating user data.");

        // create the user and add them to the db, then retrieve the claims
        // change to have sp increment id on insert.
        var claims = await _userRepository.RegisterUser(request, userData);

        return await TokenBuilder.BuildToken(
            "SuperDuperSecretValueSuperDuperSecretValue",
            "https://localhost:5000",
            "https://localhost:5000",
            claims.ToList(),
            userData
            );
    }

    public async Task<GetUserDataResponse> LoginUser(LoginRequest request)
    {
        // find if the user is valid, if they are create the claims or retrieve them from the db
        // replace with sp that returns userid
        var alreadyExists = await _userRepository.CheckIfUserExists(request.Password, request.Username);
        if (!alreadyExists)
            throw new Exception("Incorrect or unknown credentials");
        
        var claims = new List<Claim>()
        {
            new (ClaimTypes.Role, "Admin"),
            new (ClaimTypes.Name, "NADROJ"), // Replace with user claims
            new (ClaimTypes.Email, "Jordanhsheldon@gmail.com"),
        };

        return await TokenBuilder.BuildToken(
            "SuperDuperSecretValueSuperDuperSecretValue",// get key from config
            "https://localhost:5000",// get url from config
            "https://localhost:5000",// get url from config
            claims.ToList(),
            request.Username
            );
    }
}
