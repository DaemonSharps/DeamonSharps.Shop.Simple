"use strict"
{
    let path = require("path");

    const CleanDirWebpackPlugin = require("cleandir-webpack-plugin");
    const MiniCssExtractPlugin = require('mini-css-extract-plugin');

    const wwwroot = "wwwroot/";
    const siteCssFolder = "css/";
    const siteJSFolder = "js/";
    const siteBootstrapFolder = "bootstrap/";
    const bootstrapCssFolder = "./node_modules/bootstrap/dist/css/";

    module.exports = {
        entry: {
            [siteCssFolder + 'styles']: "./wwwroot/lib/ShopCss/StyleSheet.css",
            [siteJSFolder + '/scripts']: "./wwwroot/lib/ShopJS/site.js",
            [siteBootstrapFolder + '/bootstrap']: [
                bootstrapCssFolder + "bootstrap.css"
            ]
            },

        output: {
            filename: "[name].min.js",
            path: path.resolve(__dirname, wwwroot)
        },

        plugins: [
            new CleanDirWebpackPlugin([
                wwwroot + siteBootstrapFolder + "*",
                wwwroot + siteCssFolder + "*",
                wwwroot + siteJSFolder + "*"
            ]),
            new MiniCssExtractPlugin()
        ],
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [MiniCssExtractPlugin.loader, "css-loader"]
                }
            ]
        },
        mode: 'none',
        optimization: {
            minimize: false
        }
    };
}

    
