const defaultTheme = require("tailwindcss/defaultTheme");

module.exports = {
    purge: {
        enabled: false,
        content: [
            "./Pages/**/*.razor", 
            "./Shared/**/*.razor", 
            "./Components/**/*.razor"
        ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            fontFamily: {
                sans: [
                    'Segoe UI', 
                    'Calibri', 
                    'Helvetica', 
                    ...defaultTheme.fontFamily.sans
                ],
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms')
    ],
}
