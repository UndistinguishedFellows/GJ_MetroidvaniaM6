using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : SerializedMonoBehaviour
{
    public SortedCallbackList<float> OnEarlyUpdate { get; } = new SortedCallbackList<float>();
    public SortedCallbackList<float> OnUpdate { get; } = new SortedCallbackList<float>();
    public SortedCallbackList<float> OnLateUpdate { get; } = new SortedCallbackList<float>();
    public SortedCallbackList<float> OnFixedUpdate { get; } = new SortedCallbackList<float>();

    // ==================================================

    public void AddBehaviour(BaseBehaviour behaviour)
    {
        if(behaviour == null)
            return;
        
        behaviour.Init(this, m_playerState);
        m_pendingToAddBehaviours.Add(behaviour);
    }
    
    // ==================================================
    
    private void Awake()
    {
        m_playerState= new PlayerState();
        m_playerState.Init(this);
    }
    
    private void Start()
    {
        m_behaviours.ForEach(x => x.Start());
    }

    private void Update()
    {
        if (m_pendingToAddBehaviours.Count > 0)
        {
            m_pendingToAddBehaviours.ForEach(x => x.Start());
            m_behaviours.AddRange(m_pendingToAddBehaviours);
            m_pendingToAddBehaviours.Clear();
        }

        float dt = Time.deltaTime;
        
        OnEarlyUpdate.Invoke(dt);
        ProcessInputCallbacks(dt);
        OnUpdate.Invoke(dt);
    }

    private void LateUpdate()
    {
        OnLateUpdate.Invoke(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        OnFixedUpdate.Invoke(Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        m_behaviours.ForEach(x => x.OnActivate());
    }
    
    private void OnDisable()
    {
        m_behaviours.ForEach(x => x.OnDeactivate());
    }

    private void OnDestroy()
    {
        m_pendingToAddBehaviours.Clear();
        m_behaviours.ForEach(x => x.CleanUp());
    }
    
    // ==================================================

    private void ProcessInputCallbacks(float dt)
    {
        
    }
    
    // ==================================================
    
    [SerializeField] private List<BaseBehaviour> m_behaviours = new List<BaseBehaviour>();
    private List<BaseBehaviour> m_pendingToAddBehaviours = new List<BaseBehaviour>();

    private PlayerState m_playerState = null;
}
