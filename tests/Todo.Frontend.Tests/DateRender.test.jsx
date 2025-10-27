import { describe, test, expect } from "@jest/globals";

describe("Date rendering", () => {
  test("renders a local time string", () => {
    const iso = "2025-10-24T03:45:10Z";
    const text = new Date(iso).toLocaleString();
    expect(typeof text).toBe("string");
    expect(text.length).toBeGreaterThan(5);
  });
});
