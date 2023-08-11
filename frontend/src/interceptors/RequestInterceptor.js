export const requestInterceptor = (request) => {
  const token = localStorage.getItem("userToken");
  if (token) {
    request.headers["Authorization"] = "Bearer " + token;
  }
  return request;
};

export default requestInterceptor;
