module.exports = {
    extends: ['@commitlint/config-conventional'],
    rules: {
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
