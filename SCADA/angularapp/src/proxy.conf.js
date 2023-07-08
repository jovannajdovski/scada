const PROXY_CONFIG = [
  {
    context: [
      "/scada",
    ],
    target: "http://localhost:5163",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
