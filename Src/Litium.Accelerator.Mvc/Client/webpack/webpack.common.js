const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const path = require('path');
const ROOT = path.resolve(__dirname, '../');
const BUILD_DIR = path.resolve(ROOT, '../wwwroot/ui');

module.exports = {
    context: ROOT,
    output: {
        path: BUILD_DIR,
        publicPath: '/ui/',
        filename: '[name].js',
        clean: true,
    },
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.css$/,
                include: /node_modules/,
                use: [
                    {
                        loader: 'style-loader',
                    },
                    {
                        loader: 'css-loader',
                        options: {
                            sourceMap: true,
                        },
                    },
                ],
            },
            {
                test: /\.scss$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                    },
                    {
                        loader: 'css-loader', // translates CSS into CommonJS
                        options: {
                            sourceMap: true,
                        },
                    },
                    {
                        loader: 'resolve-url-loader', // resolve relative url of assets in scss files if they are imported from node_modules
                    },
                    {
                        loader: 'sass-loader', // compiles Sass to CSS, using Dart Sass
                        options: {
                            sourceMap: true,
                        },
                    },
                ],
            },
            {
                // PandoNexis Modification
                // yarn add --dev @svgr/webpack
                // https://react-svgr.com/docs/webpack/
                test: /\.svg$/i,
                issuer: /\.[jt]sx?$/,
                resourceQuery: /component/, // *.svg?component
                use: ['@svgr/webpack'],
            },
            {
                test: /\.svg$/,
                exclude: /(\/fonts)/,
                resourceQuery: { not: [/component/] }, // ADDED PandoNexis Modification // exclude inline URL if *.svg?component
                type: 'asset/inline',
            },
            {
                test: /\.(png|jpg|jpeg|gif|ico)/i,
                exclude: /(\/fonts)/,
                type: 'asset/resource',
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)/i,
                include: [/fonts/, /node_modules/],
                type: 'asset/resource',
            },
        ],
    },
    plugins: [],
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },
};
