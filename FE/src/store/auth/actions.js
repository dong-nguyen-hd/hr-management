import { api } from "src/boot/axios";
import { jwtDecode } from "jwt-decode";

export const validateToken = async ({ dispatch, getters }) => {
  let token = getters.getToken.accessToken;
  let { exp } = jwtDecode(token);
  let timeUntilRefresh = exp - Date.now() / 1000;

  let result = false;
  if (timeUntilRefresh < 30) result = await dispatch("useRefreshToken");
  else return true;

  return result;
};

export const login = async ({ commit, dispatch }, payload) => {
  let resource = payload.resource;

  // Assign data to localStorage
  localStorage.setItem("hraccountid", JSON.stringify(resource.id));
  localStorage.setItem(
    "hraccounttoken",
    JSON.stringify(resource.tokenResource)
  );

  commit("setToken", resource.tokenResource);
  commit("setInformation", resource);
  dispatch("setHeaderJWT");
};

export const logOut = async ({ commit, dispatch, getters }) => {
  let tempToken = getters.getToken;
  let payload = {
    id: tempToken.id,
    refreshToken: tempToken.refreshToken,
  };

  let result = await api
    .post(`/api/v1/token/logout`, payload)
    .then((response) => {
      return response.data;
    })
    .catch(function (error) {
      // Checking if throw error
      if (error.response) {
        // Server response
        return error.response.data;
      } else {
        // Server not working
        let temp = { success: false, message: ["Server Error!"] };
        return temp;
      }
    });

  localStorage.removeItem("hraccounttoken");
  localStorage.removeItem("hraccountid");
  commit("removeToken");
  dispatch("setHeaderJWT");
};

export const getInformation = async ({ commit }) => {
  let idStorage = parseInt(localStorage.getItem("hraccountid"));

  let result = await api
    .get(`/api/v1/account/${idStorage}`)
    .then((response) => {
      return response.data;
    })
    .catch(function (error) {
      // Checking if throw error
      if (error.response) {
        // Server response
        return error.response.data;
      } else {
        // Server not working
        let temp = { success: false, message: ["Server Error!"] };
        return temp;
      }
    });

  if (result.success) commit("setInformation", result.resource);

  return result.success;
};

export const useRefreshToken = async ({ dispatch, commit }) => {
  let idStorage = parseInt(localStorage.getItem("hraccountid"));
  let tokenStorage = JSON.parse(localStorage.getItem("hraccounttoken"));

  let payload = {
    id: tokenStorage.id,
    refreshToken: tokenStorage.refreshToken,
    accountId: idStorage,
  };

  let result = await api
    .post("/api/v1/token/refresh-token", payload)
    .then((response) => {
      return response.data;
    })
    .catch(function (error) {
      // Checking if throw error
      if (error.response) {
        // Server response
        return error.response.data;
      } else {
        // Server not working
        let temp = { success: false, message: ["Server Error!"] };
        return temp;
      }
    });

  if (result.success) {
    commit("setRole", result.resource.role);
    
    // Assign data to localStorage
    localStorage.setItem("hraccounttoken", JSON.stringify(result.resource));
    commit("setToken", result.resource);

    dispatch("setHeaderJWT");
  } else {
    commit("removeToken");
    dispatch("setHeaderJWT");
  }

  return result.success;
};

export const init = async ({ commit, dispatch }) => {
  let tokenStorage = JSON.parse(localStorage.getItem("hraccounttoken"));
  let isSuccess = false;

  if (tokenStorage) {
    let response = await dispatch("useRefreshToken");
    if (response) isSuccess = true;
  }

  if (isSuccess) await dispatch("getInformation");
  else commit("removeToken");
};

export const setHeaderJWT = async ({ getters }) => {
  let isAuthenticated = getters.isAuthenticated;

  if (isAuthenticated) {
    let tempToken = getters.getToken;
    api.defaults.headers.common.Authorization =
      "Bearer " + tempToken.accessToken;
  } else api.defaults.headers.common.Authorization = "";
};
