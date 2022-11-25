/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.tsx',
  ],
  theme: {
    fontSize: {
      xs: 14,
      sm: 16,
      md: 18,
      ld: 20,
      xl: 24,
      xxl: 32,
    },
    colors:{
      'gray-900':'#111827',
      'gray-800':'#1e293b',
      'gray-400':'#9ca3af',
      'gray-200':'#e5e7eb',
      'gray-100':'#f1f5f9',

      'cyan-500': '#06b6d4',
      'cyan-300': '#67e8f9'
    },

    extend: {
      fontFamily:{
        sans: 'Inter, sans-serif'
      }
    },
  },
  plugins: [],
}
