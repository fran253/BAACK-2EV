using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class PreguntaService : IPreguntaService
    {
        private readonly IPreguntaRepository _preguntaRepository;

        public PreguntaService(IPreguntaRepository preguntaRepository)
        {
            _preguntaRepository = preguntaRepository;
        }

        public async Task<List<Pregunta>> GetAllAsync()
        {
            return await _preguntaRepository.GetAllAsync();
        }

        public async Task<Pregunta?> GetByIdAsync(int id)
        {
            return await _preguntaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Pregunta pregunta)
        {
            if (pregunta == null)
            {
                throw new ArgumentNullException(nameof(pregunta), "la pregunta no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(pregunta.Enunciado))
            {
                throw new ArgumentException("el enunciado de la pregunta no puede estar vacío.", nameof(pregunta.Enunciado));
            }

            if (pregunta.Test == null || pregunta.Test.IdTest <= 0)
            {
                throw new ArgumentException("el test asociado a la pregunta no es válido.", nameof(pregunta.Test));
            }

            await _preguntaRepository.AddAsync(pregunta);
        }

        public async Task UpdateAsync(Pregunta pregunta)
        {
            var existingPregunta = await _preguntaRepository.GetByIdAsync(pregunta.IdPregunta);
            if (existingPregunta == null)
            {
                throw new KeyNotFoundException("pregunta no encontrada.");
            }

            if (string.IsNullOrWhiteSpace(pregunta.Enunciado))
            {
                throw new ArgumentException("el enunciado de la pregunta no puede estar vacío.", nameof(pregunta.Enunciado));
            }

            if (pregunta.Test == null || pregunta.Test.IdTest <= 0)
            {
                throw new ArgumentException("el test asociado a la pregunta no es válido.", nameof(pregunta.Test));
            }

            await _preguntaRepository.UpdateAsync(pregunta);
        }

        public async Task DeleteAsync(int id)
        {
            var pregunta = await _preguntaRepository.GetByIdAsync(id);
            if (pregunta == null)
            {
                throw new KeyNotFoundException("pregunta no encontrada.");
            }

            await _preguntaRepository.DeleteAsync(id);
        }
    }
}
