<!-- AvatarRiendo.vue -->
<template>
    <div class="avatar-container">
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 200 200">
        <!-- Cuerpo/bola naranja -->
        <circle cx="100" cy="100" r="80" fill="#FF7A00" />
        
        <!-- Ojos simples -->
        <ellipse cx="70" cy="80" rx="10" ry="12" fill="white" />
        <ellipse cx="130" cy="80" rx="10" ry="12" fill="white" />
        
        <!-- Cejas expresivas -->
        <path d="M60 65 Q70 55 80 65" stroke="white" stroke-width="4" stroke-linecap="round" fill="none" />
        <path d="M120 65 Q130 55 140 65" stroke="white" stroke-width="4" stroke-linecap="round" fill="none" />
        
        <!-- Boca animada abriendo y cerrando (riendo) -->
        <path d="M70 120 Q100 150 130 120 Q100 140 70 120" fill="white">
          <animate 
            attributeName="d" 
            dur="0.5s"
            repeatCount="indefinite"
            values="M70 120 Q100 150 130 120 Q100 140 70 120;
                    M70 120 Q100 130 130 120 Q100 120 70 120;
                    M70 120 Q100 150 130 120 Q100 140 70 120"
            keyTimes="0; 0.5; 1" />
        </path>
      </svg>
    </div>
  </template>
  
  <style scoped>
  .avatar-container {
    width: 100px;
    height: 100px;
  }
  </style>