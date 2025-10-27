/** @type {import('jest').Config} */
module.exports = {
  testEnvironment: "jest-environment-jsdom",
  setupFilesAfterEnv: ["<rootDir>/setupTests.js"],
  testMatch: ["**/*.test.jsx", "**/*.spec.jsx", "**/*.test.js"],

  // Let Jest “see” the frontend folder too (optional but helps)
  roots: [
    "<rootDir>",
    "<rootDir>/../../frontend/src"
  ],

  moduleNameMapper: {
    "^react$": "<rootDir>/../../frontend/node_modules/react",
    "^react-dom$": "<rootDir>/../../frontend/node_modules/react-dom",
    "\\.(css|less|sass|scss)$": "<rootDir>/__mocks__/styleMock.js",
    "^@frontend/(.*)$": "<rootDir>/../../frontend/src/$1"
  },

  // Use Babel so Istanbul can instrument coverage
  transform: {
    "^.+\\.(js|jsx)$": "babel-jest"
  },
  transformIgnorePatterns: ["/node_modules/"],

  // ✅ Count coverage from the frontend source
  collectCoverage: true,
  collectCoverageFrom: [
    "<rootDir>/../../frontend/src/**/*.{js,jsx}",
    "!**/node_modules/**",
    "!**/*.d.ts"
  ],
  coverageProvider: "babel",
  coverageReporters: ["text", "html", "lcov"],
  coverageDirectory: "<rootDir>/coverage",
  coveragePathIgnorePatterns: [
    "/node_modules/",
    "<rootDir>/coverage/",
    "<rootDir>/__mocks__/"
  ]
};
