var webpack = require("webpack");
var path = require("path");
var DEV = path.resolve(__dirname, "ClientApp");
var bundleOutputDir = './wwwroot/dist';

var config = {
    entry: { 'main': './ClientApp/index.jsx' },
    output: {
        path: path.join(__dirname, bundleOutputDir),
        filename: '[name].js',
        publicPath: 'dist/'
    },
    module: {
        loaders: [{
            include: DEV,
            loader: "babel-loader"
        }]
    }
};
module.exports = config;