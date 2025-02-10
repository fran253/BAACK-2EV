using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class ResultadoService : IResultadoService
    {
        private readonly IResultadoRepository _resultadoRepository;

        public ResultadoService(IResultadoRepository resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<List<Resultado>> GetAllAsync()
        {
            return await _resultadoRepository.GetAllAsync();
        }

        public async Task<Resultado?> GetByIdAsync(int id)
        {
            return await _resultadoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Resultado resultado)
        {
            if (resultado == null)
                throw new ArgumentNullException(nameof(resultado), "el resultado no puede ser nulo.");

            if (resultado.Puntuacion < 0)
                throw new ArgumentException("la puntuación no puede ser negativa.", nameof(resultado.Puntuacion));

            if (resultado.Usuario == null || resultado.Usuario.IdUsuario <= 0)
                throw new ArgumentException("el usuario asociado al resultado no es válido.", nameof(resultado.Usuario));

            if (resultado.Pregunta == null || resultado.Pregunta.IdPregunta <= 0)
                throw new ArgumentException("la pregunta asociada al resultado no es válida.", nameof(resultado.Pregunta));

            await _resultadoRepository.AddAsync(resultado);
        }

        public async Task UpdateAsync(Resultado resultado)
        {
            var existingResultado = await _resultadoRepository.GetByIdAsync(resultado.IdResultado);
            if (existingResultado == null)
                throw new KeyNotFoundException("resultado no encontrado.");

            if (resultado.Puntuacion < 0)
                throw new ArgumentException("la puntuación no puede ser negativa.", nameof(resultado.Puntuacion));

            if (resultado.Usuario == null || resultado.Usuario.IdUsuario <= 0)
                throw new ArgumentException("el usuario asociado al resultado no es válido.", nameof(resultado.Usuario));

            if (resultado.Pregunta == null || resultado.Pregunta.IdPregunta <= 0)
                throw new ArgumentException("la pregunta asociada al resultado no es válida.", nameof(resultado.Pregunta));

            await _resultadoRepository.UpdateAsync(resultado);
        }

        public async Task DeleteAsync(int id)
        {
            var resultado = await _resultadoRepository.GetByIdAsync(id);
            if (resultado == null)
                throw new KeyNotFoundException("resultado no encontrado.");

            await _resultadoRepository.DeleteAsync(id);
        }
    }
}
