import { defineConfig, devices } from '@playwright/test';
import dotenv from 'dotenv';

// Load environment variables from .env file
dotenv.config();

export default defineConfig({
  testDir: './tests',
  testMatch: [
    '**/*.spec.ts',
    '**/*.test.ts',
    'tests/mysecondtest.ts',
  ],
  timeout: 60000,
  expect: {
    timeout: 30000
  },
  fullyParallel: false,
  forbidOnly: !!process.env.CI,
  retries: process.env.CI ? 2 : 0,
  workers: process.env.CI ? 1 : undefined,
  reporter: [
    ['line'],
    ['allure-playwright', {
      detail: true,
      outputFolder: 'allure-results',
      suiteTitle: false
    }]
  ],
  use: {
    actionTimeout: 0,
    trace: 'on-first-retry',
    screenshot: 'on',
    video: 'on-first-retry',
    // clientCertificates : [{
    //   origin: 'https://certauth.login.microsoftonline.com/c0219094-a70e-402c-8dd2-fd89f7d64010/certauth',
    // }]
  },
  
  projects: [
    {
      name: 'Microsoft Edge',
      use: {
        ...devices['Desktop Edge'],
        channel: 'msedge',
        headless: false
      },
    }
  ]
});
