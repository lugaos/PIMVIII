package com.app.myapplication.model;

public class PlaylistModelo {
    private String nome;
    private String imagemUrl;
    private int qtdMusicas;

    public PlaylistModelo(String nome, String imagemUrl, int qtdMusicas) {
        this.nome = nome;
        this.imagemUrl = imagemUrl;
        this.qtdMusicas = qtdMusicas;
    }

    // Getters
    public String getNome() { return nome; }
    public String getImagemUrl() { return imagemUrl; }
    public int getQtdMusicas() { return qtdMusicas; }
}
