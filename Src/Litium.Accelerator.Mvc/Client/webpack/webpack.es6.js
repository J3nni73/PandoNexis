const path = require('path');
const RemovePlugin = require('remove-files-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer')
    .BundleAnalyzerPlugin;

const ROOT = path.resolve(__dirname, '../');
const BUILD_DIR = path.resolve(ROOT, '../wwwroot/ui');
const JS_DIR = path.resolve(ROOT, 'Scripts');
const CSS_DIR = path.resolve(ROOT, 'Styles');

const common = require('./webpack.common.js');
const { merge } = require('webpack-merge');

// PandoNexis:
//      Skapa flera CSSer genom att skapa nya entry-Arrays -- Dess namn är den som kommer att skapas under '/wwwroot/ui/css/[namn].min.css'
//      Dessa har sina egna namn de lyssnar på genom path.resolve

module.exports = merge(common, {
    target: ['web'],
    entry: {
        app: [
            path.resolve(JS_DIR, 'index.js'),
        ],
        site: [
            path.resolve(CSS_DIR, 'site.scss'),
        ],
        sitenoerp: [
            path.resolve(CSS_DIR, 'sitenoerp.scss'), // PandoNexis Added
        ],
    },
    output: {
        path: path.resolve(BUILD_DIR, 'es6'),
        publicPath: '/ui/es6/',
    },
    module: {
        rules: [
            {
                test: /\.js(x?)$/,
                include: JS_DIR,
                use: [
                    {
                        loader: 'babel-loader',
                        options: {
                            presets: ['@babel/preset-react'],
                            plugins: [],
                        },
                    },
                ],
            },
        ],
    },
    plugins: [
        new RemovePlugin({
            before: {
                include: [BUILD_DIR],
                log: false,
            },
        }),
        new MiniCssExtractPlugin({
            filename: '../css/[name].min.css', // PandoNexis Changed
        }),
        // new BundleAnalyzerPlugin(),
    ],
});
