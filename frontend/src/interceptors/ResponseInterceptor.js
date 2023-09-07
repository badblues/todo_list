export const responseErrorInterceptor = (error, refreshLogin, logout) => {
  if (error.response?.status === 401) {
    refreshLogin();
    throw error;
  }
  if (error.response?.status === 403) {
    logout();
  }
};

export default responseErrorInterceptor;
