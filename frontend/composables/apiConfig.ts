const myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Cookie", ".Tunnels.Relay.WebForwarding.Cookies=CfDJ8Cs4yarcs6pKkdu0hlKHsZufABc27j5DPlSJR9Wp0aLbjkn9EeCLAkDUNnPIXPovdJi_M8zcl081qT1t89tyaSGXfpMX4hCwdIBK4s7Yy7eQeTrFqKgenq14F46emxue75YELAK8xlkL1X0awSEBi0SxhGSZs87vl5JzkafG8u-p0dT0Z-LnKJ58qZyVsL3MT2t5NlBj55IiC4c8Gnsi0truR91kJUJuGOGK5b3wo9CQQb-kNE5SxqRlqjGndK1hxN6-Ir7QBMJ3wVcXkKeVsdfl0-4lSQOGFZKrp0pPme2guQDyeVmxA6Okv09qth0Y6Rt6-qjHgqPPNnea5LVbj97_VefOlFWAO9j-rjWWUpkPmpIQhuG60ND-ad8vXWW_twMbheKXImMD6xJ8tajvrY2oHypwZp93prAunqhox1JL7rTKA_me9M3_j_SDZMpJYvCcc5c9k9ztvX89U83EvKa9JLfWVjJSwNuotmuzQYzY0Y4JLidVnRrDy0HzmM54Zh-l1MbBdOtfs38g81P81cAX68K5ssiK3AGUOE4FuVGxDWpK5YBn1medOfmNGj7FzMx5NASWis1c75NlBLYpMK8nF8tai2iP5JmNIobSYCXGK9PhbPbsCbzCcd6zOp7e4PcsMvymRqlcaoh2sgUu3p3Vohp-VEy8xlD1PXFFmpgbhUF7QgHlNn48YsI-GAmA3ZOj5fVPQfWp-LQ8ws4N7D_MQ7ufAzxpkR6rYmHu2f0p2NoXjWOSJvrMzt8Ww_FTs-chIp9vQdcB8CFpMpxeqj4zKFdNjWIvSeEfNnj_P-2dOUhW4dlnqSoXalYoM-D96cp1RobIm162dyUB6jLN2zY4NEDzMjnAuzCojNtBXOKzk4OQuUJx2KgdFAQN_z0qlADbjQUSStkiHlffiGQiczokWm_m0p06y2h-67gkgsbnu2x4blqMnw8Z82tVXhlCCAOy4WXy40lDqeE_KZqIidkLcUPnpdQATeiiN8TRlMFm");

// http://localhost:5054
const urlBase: string = "https://stunning-capybara-754w979wx96f4xq-5054.app.github.dev";

/**
 * adicione o `id` para alguns methodos, leia o doc
 */
export const urls={
  categories: `${urlBase}/api/categories/`,
  products: `${urlBase}/api/products/`,
  productCategory:`${urlBase}/api/products/category/`
}

export async function customFetch(method:"GET"|"POST"|"PUT"|"DELETE",url:string,data?:Object) {
  return await fetch(url,{
    method: method || "GET",
    headers: myHeaders,
    body: JSON.stringify(data),
    redirect: "follow"
  })
}