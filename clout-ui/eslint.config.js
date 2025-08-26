import js from '@eslint/js'
import globals from 'globals'
import tseslint from 'typescript-eslint'
import pluginReact from 'eslint-plugin-react'
import json from '@eslint/json'
import { defineConfig, globalIgnores } from 'eslint/config'
import eslintConfigPrettier from 'eslint-config-prettier/flat'
import hooksPlugin from 'eslint-plugin-react-hooks'
import importPlugin from 'eslint-plugin-import'

const fileList = ['**/*.{js,mjs,cjs,ts,jsx,tsx}']

export default defineConfig([
  {
    settings: {
      react: {
        version: 'detect',
      },
      'import/extensions': ['.ts', '.tsx'],
      'import/resolver': {
        typescript: {
          project: './tsconfig.json',
        },
      },
    },
  },
  globalIgnores([
    '.react-router/',
    'build/',
    'package-lock.json',
    '.storybook/',
    'storybook-static/',
  ]),
  { files: fileList, plugins: { js }, extends: ['js/recommended'] },
  { files: fileList, languageOptions: { globals: globals.browser } },
  { ...pluginReact.configs.flat.recommended, files: fileList },
  { ...pluginReact.configs.flat['jsx-runtime'], files: fileList },
  tseslint.configs.recommended,
  {
    files: ['**/*.json'],
    plugins: { json },
    language: 'json/json',
    extends: ['json/recommended'],
  },
  eslintConfigPrettier,
  {
    plugins: {
      'react-hooks': hooksPlugin,
      import: importPlugin,
      react: pluginReact,
    },
    rules: {
      ...hooksPlugin.configs.recommended.rules,
      eqeqeq: 'error',
      'no-console': 'error',
      'react/jsx-curly-brace-presence': 'error',
      'import/no-unresolved': ['error', { commonjs: true, amd: true }],
      'no-restricted-syntax': ['error', 'FunctionDeclaration', 'FunctionExpression'],
      'prefer-arrow-callback': [
        'error',
        {
          allowNamedFunctions: false,
        },
      ],
    },
  },
])
