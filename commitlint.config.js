module.exports = {
    extends: ['@commitlint/config-conventional'],
    rules: {
        "header-case": [1, "always", "sentence-case"],
        "header-max-length": [2, "always", 100],
        "scope-enum": _ => [
            2,
            "always",
            [
                "deps",
                "docs",
                "core",
                "request",
                "response",
            ]
        ]
    }
}
