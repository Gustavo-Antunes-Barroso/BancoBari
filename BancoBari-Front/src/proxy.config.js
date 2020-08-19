const proxy = [
  {
    context: '/api',
    target: 'http://localhost:52934/api',
    pathRewrite: {'^/api' : ''}
  }
];