using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cscrud
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly DbConnectionManager _connectionManager;

        public PessoaController(DbConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        }

        [HttpPost("create")]
        public IActionResult CriarPessoas([FromBody] List<PessoaModel> pessoas)
        {
            try
            {
                using (MySqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var pessoa in pessoas)
                        {
                            string query = @"INSERT INTO Pessoa (nome, idade, cpf, email) 
                                             VALUES (@nome, @idade, @cpf, @email)";

                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@nome", pessoa.Nome);
                            command.Parameters.AddWithValue("@idade", pessoa.Idade);
                            command.Parameters.AddWithValue("@cpf", pessoa.CPF);
                            command.Parameters.AddWithValue("@email", pessoa.Email);

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }

                    var message = new { Message = "Pessoa(s) criadas com sucesso!" };

                    var jsonMessage = JsonConvert.SerializeObject(message);

                    return Ok(jsonMessage);
                }
            }
            catch (MySqlException ex)
            {
                var message = new { Message = $"Erro ao criar pessoas: {ex.Message}" };

                var jsonMessage = JsonConvert.SerializeObject(message);

                return StatusCode(500, jsonMessage);
            }
            catch (Exception ex)
            {
                var message = new { Message = $"Erro ao criar pessoas: {ex.Message}" };

                var jsonMessage = JsonConvert.SerializeObject(message);

                return StatusCode(500, jsonMessage);
            }
        }

        [HttpGet("listar")]
        public IActionResult ListarPessoas([FromQuery] string? cpf)
        {
            try
            {
                using (MySqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();

                    string query = string.IsNullOrEmpty(cpf) ?
                        "SELECT * FROM Pessoa" :
                        "SELECT * FROM Pessoa WHERE cpf = @cpf";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    if (!string.IsNullOrEmpty(cpf))
                        command.Parameters.AddWithValue("@cpf", cpf);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<PessoaModel> pessoas = new List<PessoaModel>();

                        while (reader.Read())
                        {
                            PessoaModel pessoa = new PessoaModel
                            {
                                Nome = reader.GetString("nome"),
                                Idade = reader.GetInt32("idade"),
                                CPF = reader.GetString("cpf"),
                                Email = reader.GetString("email")
                            };
                            pessoas.Add(pessoa);
                        }

                        return Ok(pessoas);
                    }
                }
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, $"Erro ao listar pessoas: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        [HttpPut("atualizar")]
        public IActionResult AtualizarPessoa(string cpf, [FromBody] PessoaModel pessoa)
        {
            try
            {
                using (MySqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE Pessoa SET nome = @nome, idade = @idade, email = @email WHERE cpf = @cpf";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@idade", pessoa.Idade);
                    command.Parameters.AddWithValue("@cpf", cpf);
                    command.Parameters.AddWithValue("@email", pessoa.Email);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        var message = new { Message = "Pessoa atualizada!" };

                        var jsonMessage = JsonConvert.SerializeObject(message);

                        return Ok(jsonMessage);
                    }
                    else
                    {
                        return NotFound("Não foi possível encontrar a pessoa com o CPF fornecido.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, $"Erro ao atualizar pessoa: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        [HttpDelete("deletar")]
        public IActionResult DeletarPessoa(string cpf)
        {
            try
            {
                using (MySqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();

                    string query = "DELETE FROM Pessoa WHERE cpf = @cpf";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cpf", cpf);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        var message = new { Message = "Pessoa deletada com sucesso." };

                        var jsonMessage = JsonConvert.SerializeObject(message);

                        return Ok(jsonMessage);
                    }
                    else
                    {
                        return NotFound("Não foi possível encontrar a pessoa com o CPF fornecido.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, $"Erro ao deletar pessoa: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
