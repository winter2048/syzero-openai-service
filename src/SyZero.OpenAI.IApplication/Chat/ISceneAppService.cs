using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.Application.Routing;
using SyZero.OpenAI.IApplication.Chat.Dto;

namespace SyZero.OpenAI.IApplication.Chat
{
    public interface ISceneAppService : IApplicationServiceBase
    {
        /// <summary>
        /// 创建场景
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.POST, "/api/SyZero.OpenAI/Scene")]
        Task<SceneDto> CreateScene(SceneDto dto);

        /// <summary>
        /// 修改场景
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.PUT, "/api/SyZero.OpenAI/Scene/{sceneId}")]
        Task<SceneDto> PutScene(string sceneId, SceneDto dto);

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.GET, "/api/SyZero.OpenAI/Scene/{sceneId}")]
        Task<SceneDto> GetScene(string sceneId);

        /// <summary>
        /// 获取我的所以场景
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.GET, "/api/SyZero.OpenAI/Scenes")]
        Task<List<SceneDto>> MyScene();

        /// <summary>
        /// 删除场景
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.DELETE, "/api/SyZero.OpenAI/Scene/{sceneId}")]
        Task<bool>DelScene(string sceneId);
    }
}
